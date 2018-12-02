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

        public String CsvPath;
        public CSVSaver(String CsvPath)
        {
            this.CsvPath = CsvPath;

            if (!File.Exists(CsvPath))
            {
                writeHeader();
            }

        }

        public void writeHeader()
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

                    //var result = Encoding.UTF8.GetString(mem.ToArray());
                    //Console.WriteLine(result);
                    //return csvWriter;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Nie mozna zapisac - zamknij plik z danymi");
            }
        }
    }
}
