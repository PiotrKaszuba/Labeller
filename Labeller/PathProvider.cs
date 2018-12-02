using Labeller.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labeller
{
    public class PathProvider
    {
        public List<String> paths;
        public int currentPath;
        public CSVSaver CSVSaver { get; set; }
        public PathProvider(CSVSaver cSVSaver)
       
        {
            this.CSVSaver = cSVSaver;
            paths = new List<string>();
            currentPath = -1;
        }

        public String changePath(int change)
        {
            currentPath += change;
            return getCurrentPath();
        }
        private String run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = Resources.pythonPath;
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                 
                    return result.Replace("\r\n", "");
                }
            }
        }
        private bool analyzePath(String path)
        {
            InformationCSVMerger<CSVRecord> informationCSVMerger = new InformationCSVMerger<CSVRecord>(CSVSaver.CsvPath);
            if (InformationCSVMerger<CSVRecord>.getRecord(path, informationCSVMerger.getRecords()) == null) return true;
            else return false;
        }
        private String getPath(int times = 0)
        {
            if(times >100)
            {
                MessageBox.Show("Przekroczono limit losowania bez powtorzenia - zmien dane");
                Application.Exit();
            }
            String path = run_cmd(Directory.GetCurrentDirectory() + Resources.pythonPathProviderRelativePath, "");
            return analyzePath(path) ? path : getPath(times +1);
        }

        private void checkPathLength()
        {
            if (currentPath < 0)
                currentPath = 0;
            if(currentPath >= paths.Count)
            {
                paths.Add(getPath());
                checkPathLength();
            }
        }

        public String getCurrentPath()
        {
            checkPathLength();
            return paths[currentPath];
        }

    }
}
