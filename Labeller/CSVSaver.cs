using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labeller
{
    public class CSVSaver
    {
        private String deleted = null;
        public String CsvPath;
        public CSVSaver(String CsvPath)
        {
            this.CsvPath = CsvPath;

            if (!File.Exists(CsvPath))
            {
                writeHeader();
            }

        }

        private void writeHeader()
        {
            using (var writer = new StreamWriter(CsvPath, true, Encoding.UTF8))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = ",";
                csvWriter.Configuration.HasHeaderRecord = true;

                CSVStructure.writeHeader(csvWriter);
                csvWriter.NextRecord();
                writer.Flush();
            }
        }
        public void addDeleted()
        {
            if (deleted == null) return;
      
            File.AppendAllLines(CsvPath, new String[] { deleted });
            MessageBox.Show("Ponownie dopisano wiersz:\n" + deleted);
            deleted = null;
        }
        public void deleteLastRecord()
        {
            String[] lines = File.ReadAllLines(CsvPath);
            int len = lines.Length - 1;
            if (len > 0)
            {
                String[] newLines = new String[len];
                Array.Copy(lines, newLines, len);
                deleted = lines[len];
                MessageBox.Show("Usuwanie wiersza:\n" + lines[len] +"\nPozostało wierszy: "+(len-1));
                rewriteCSV(newLines);
            }

        }

        private void rewriteCSV(String[] lines)
        {
           
            try
            {
                if (File.Exists(CsvPath))
                    File.Delete(CsvPath);
                File.WriteAllLines(CsvPath, lines);
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie mozna zapisac - zamknij plik z danymi");
            }

        }
        public void saveToCSV(CSVStructure record, String path)
        {
            try
            {
                using (var writer = new StreamWriter(CsvPath, true, Encoding.UTF8))
                using (var csvWriter = new CsvWriter(writer))
                {
                    csvWriter.Configuration.Delimiter = ",";
                    csvWriter.Configuration.HasHeaderRecord = true;


                    record.writeRecord(csvWriter, path);


                    csvWriter.NextRecord();
                    writer.Flush();

                 
                }
             
            }
            catch(Exception e)
            {
                MessageBox.Show("Nie mozna zapisac - zamknij plik z danymi");
            }

            String[] lines = File.ReadAllLines(CsvPath);
            MessageBox.Show("Zapisano wiersz: \n" + lines[lines.Length - 1]);
        }
    }
}
