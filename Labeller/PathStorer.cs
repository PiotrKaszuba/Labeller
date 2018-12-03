using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labeller
{
    public class PathStorer
    {
        public List<String> paths;
        public List<CSVRecord> records;
        public int currentPath;
        public PathAnalyzer pathAnalyzer;

        public PathStorer(CSVSaver CSVSaver)
        {
            paths = new List<string>();
            currentPath = -1;
            this.pathAnalyzer = new PathAnalyzer(new PathProvider(), CSVSaver);
            this.records = new List<CSVRecord>();
        }
        public PathStorer(PathAnalyzer pathAnalyzer)
        {
            paths = new List<string>();
            currentPath = -1;
            this.records = new List<CSVRecord>();
            this.pathAnalyzer = pathAnalyzer;
        }

        public String changePath(int change, int labelledPatients = 0)
        {
            currentPath += change;
            String path =  getCurrentPath(labelledPatients);
            if (pathAnalyzer.CSVRecord != null) storeCSVRecord(pathAnalyzer.CSVRecord);
            return path;
        }
        private void checkPathLength(int labelledPatients = 0)
        {
            if (currentPath < 0)
                currentPath = 0;
            if (currentPath >= paths.Count)
            {
                paths.Add(pathAnalyzer.getPath(labelledPatients:labelledPatients));
                checkPathLength(labelledPatients);
            }
        }
        public void storeCSVRecord(CSVRecord CSVRecord)
        {
            if (records.Count <= currentPath) records.Add(CSVRecord);
            else records[currentPath] = CSVRecord;
        }
        public CSVRecord getCurrentRecord()
        {
            try
            {
                return records[currentPath];
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public void deleteLoadedRecord()
        {
            pathAnalyzer.deleteLoadedRecord();
        }
        public String getCurrentPath(int labelledPatients = 0)
        {
            checkPathLength(labelledPatients);
            return paths[currentPath];
        }
    }
}
