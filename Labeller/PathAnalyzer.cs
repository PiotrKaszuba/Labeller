using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labeller
{
    public class PathAnalyzer
    {

        public PathProvider pathProvider;
        public CSVSaver CSVSaver;
        public bool dropPolitics;
        public CSVRecord CSVRecord { get; set; } = null;
        private int loadedLabelled = 0;
        private InformationCSVMerger<CSVRecord> informationCSVMerger;
        public PathAnalyzer(PathProvider pathProvider, CSVSaver CSVSaver, bool dropPolitics = true)
        {
            this.CSVSaver = CSVSaver;
            this.pathProvider = pathProvider;
            this.dropPolitics = dropPolitics;
        }

        public String getPath(int times = 0, int labelledPatients = 0)
        {
            CSVRecord = null;
            informationCSVMerger = null;
            if (times > 10)
            {
                MessageBox.Show("Przekroczono limit losowania bez powtorzenia - zmien dane");
                Environment.FailFast("");
                return null;
            }
            String path = pathProvider.getPath();
            String final;
            if (informationCSVMerger == null) informationCSVMerger = new InformationCSVMerger<CSVRecord>(CSVSaver.CsvPath);
            if (labelledPatients > 0 && informationCSVMerger.getRecords().Count > loadedLabelled)
            {
                final = getLabelledPath();
            }
            else
            {
                if (labelledPatients > 0) MessageBox.Show("Załadowano nowego pacjenta - nie ma kolejnych rekordów danych do załadowania oznaczonych");
                final = analyzePath(path) && dropPolitics ? getPath(times + 1, labelledPatients: labelledPatients) : path;
            }
            
            informationCSVMerger = null;
            return final;
        }

        private String getLabelledPath()
        {


            CSVRecord = informationCSVMerger.getRecords().ElementAt(loadedLabelled);
            loadedLabelled++;
            String dir = PropertiesReader.PATH_PROVIDER_ADDITIONAL_ARGS + "/" + CSVRecord.Patient + "/" + CSVRecord.Date;
            String dirEye = dir + "/" + CSVRecord.Eye;
            if (Directory.Exists(dirEye))
                return dirEye;
            else
                return dir;

        }
        public void deleteLoadedRecord()
        {
            CSVSaver.deleteRecord(CSVRecord);
        }
        public bool analyzePath(String path)
        {
            if(informationCSVMerger == null)
            informationCSVMerger = new InformationCSVMerger<CSVRecord>(CSVSaver.CsvPath);
            CSVRecord = null;
            CSVRecord = InformationCSVMerger<CSVRecord>.getRecord(path, informationCSVMerger.getRecords());
           
            if (CSVRecord == null) return false;
            else return true;
        }
    }
}
