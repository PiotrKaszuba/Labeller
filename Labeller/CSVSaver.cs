using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labeller
{
    public class CSVSaver
    {
        public CSVRecord deleted = null;
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
                csvWriter.Configuration.AutoMap<CSVRecord>();
                csvWriter.WriteHeader<CSVRecord>();
                csvWriter.NextRecord();
                writer.Flush();
            }
        }
        public void addDeleted()
        {
            if (deleted == null) return;

            saveRecordsToCsv(new CSVRecord[] { deleted });
            MessageBox.Show("Ponownie dopisano wiersz:\n" + buildRecordString(deleted));
            deleted = null;
        }
        public void deleteRecord(CSVRecord record)
        {
            if (record == null) return;

            deleteTemplate(readCSV(), record);
        }

        public static String buildRecordString(CSVRecord record)
        {
            StringBuilder sb = new StringBuilder();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(record))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(record);
                sb.Append(name + ": " + value + ", ");
            }
            return sb.ToString();
        }

        private List<CSVRecord> readCSV()
        {
            InformationCSVMerger<CSVRecord> informationCSVMerger = new InformationCSVMerger<CSVRecord>(CsvPath);

            return  informationCSVMerger.getRecords();
        }
        public void deleteRecord()
        {
            List<CSVRecord> records = readCSV();
            if (records.Count == 0) return;
            CSVRecord removed = records.Last();

            deleteTemplate(records, removed);
           

        }
        private void deleteTemplate(List<CSVRecord> records, CSVRecord toDelete)
        {
            bool removed = records.Remove(toDelete);
           
            if (removed) {
                deleted = toDelete;
                String recordString = buildRecordString(toDelete);
                MessageBox.Show("Pozostało wierszy: "+ records.Count+"\nUsuwanie wiersza:\n" + recordString);
                rewriteCSV(records);
            }

            
        }
        private void rewriteCSV(List<CSVRecord> records)
        {
           
            try
            {
                if (File.Exists(CsvPath))
                    File.Delete(CsvPath);

                writeHeader();
                saveRecordsToCsv(records.ToArray());

                
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie mozna zapisac - zamknij plik z danymi");
            }

        }
        private void saveRecordsToCsv(CSVRecord[] records)
        {
            try
            {
                using (var writer = new StreamWriter(CsvPath, true, Encoding.UTF8))
                using (var csvWriter = new CsvWriter(writer))
                {
                    csvWriter.Configuration.Delimiter = ",";
                    csvWriter.Configuration.HasHeaderRecord = false;

                    csvWriter.WriteRecords<CSVRecord>(records);



                    
                    writer.Flush();


                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Nie mozna zapisac - zamknij plik z danymi");
            }
        }

        public void saveToCSV(CSVStructure record, String path)
        {
            CSVRecord rec = record.getRecordFromStructure(path);
            saveRecordsToCsv(new CSVRecord[] { rec });
            MessageBox.Show("Zapisano wiersz:\n" + buildRecordString(rec));
        }
    }
}
