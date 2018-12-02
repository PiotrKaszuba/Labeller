using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labeller
{
   public class InformationCSVMerger
    {
        public String path;
        public List<PatientRecord> recordsList;
        public InformationCSVMerger(String path)
        {
            this.path = path;
            this.recordsList = ReadInCSV(this.path);
        }

        private void openFile()
        {

        }
        public PatientRecord getRecord(string path)
        {
            String patient;
            String date;
            String[] Dirs = path.Split('/');
            if (path.Contains("eye_images"))
            {
                patient = Dirs[Dirs.Length - 4];
                date = Dirs[Dirs.Length - 3];

            }
            else
            {
                patient = Dirs[Dirs.Length -3];
                date = Dirs[Dirs.Length-2];
            }
            return recordsList.Find(x => x.md5.Equals(patient) && x.examination_date.Equals(date));
        }
        public static List<PatientRecord> ReadInCSV(string absolutePath)
        {
            using (var sr = new StreamReader(absolutePath))
            {
                var reader = new CsvReader(sr);

                //CSVReader will now read the whole file into an enumerable
                List<PatientRecord> records = reader.GetRecords<PatientRecord>().ToList();
                return records;

            }
            
           
        }


    }
}
