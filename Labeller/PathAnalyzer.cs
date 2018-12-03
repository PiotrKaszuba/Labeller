using System;
using System.Collections.Generic;
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
        public PathAnalyzer(PathProvider pathProvider, CSVSaver CSVSaver, bool dropPolitics = true)
        {
            this.CSVSaver = CSVSaver;
            this.pathProvider = pathProvider;
            this.dropPolitics = dropPolitics;
        }

        public String getPath(int times = 0)
        {
            if (times > 10)
            {
                MessageBox.Show("Przekroczono limit losowania bez powtorzenia - zmien dane");
                Environment.FailFast("");
                return null;
            }
            String path = pathProvider.getPath();
            return analyzePath(path) && dropPolitics ? getPath(times + 1) : path;
        }
        public void deleteLoadedRecord()
        {
            CSVSaver.deleteRecord(CSVRecord);
        }
        public bool analyzePath(String path)
        {
            InformationCSVMerger<CSVRecord> informationCSVMerger = new InformationCSVMerger<CSVRecord>(CSVSaver.CsvPath);
            CSVRecord = null;
            CSVRecord = InformationCSVMerger<CSVRecord>.getRecord(path, informationCSVMerger.getRecords());
            if (CSVRecord == null) return false;
            else return true;
        }
    }
}
