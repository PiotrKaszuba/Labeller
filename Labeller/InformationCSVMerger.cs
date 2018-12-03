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
    
   public class InformationCSVMerger<T>
    {
        public String path;
        private List<T> records;

        public List<T> getRecords()
        {
            if(records == null)
            {
                this.records = ReadInCSV(this.path);
            }
            return records;
        }

        public InformationCSVMerger(String path)
        {
            this.path = path;
        }

     
        public static PatientRecord getRecord(string path, List<PatientRecord> list)
        {
            String patient = Utils.getPatient(path);
            String date = Utils.getDate(path);

            return list.Find(x => x.md5.Equals(patient) && x.examination_date.Equals(date));
        }

        public static CSVRecord getRecord(string path, List<CSVRecord> list)
        {
            String patient = Utils.getPatient(path);
            String date = Utils.getDate(path);
           

            return list.Find(x => x.Patient.Equals(patient) && x.Date.Equals(date));
        }


     

        public List<T> ReadInCSV(string absolutePath)
        {
            try
            {
                using (var sr = new StreamReader(absolutePath))
                {
                    var reader = new CsvReader(sr);


                    List<T> records = reader.GetRecords<T>().ToList();
                    return records;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie mozna odczytac - zamknij plik z danymi");
                return ReadInCSV(absolutePath);
            }


        }






    }
}
