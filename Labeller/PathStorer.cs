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
        public int currentPath;
        public PathAnalyzer pathAnalyzer;

        public PathStorer(CSVSaver CSVSaver)
        {
            paths = new List<string>();
            currentPath = -1;
            this.pathAnalyzer = new PathAnalyzer(new PathProvider(), CSVSaver);

        }
        public PathStorer(PathAnalyzer pathAnalyzer)
        {
            paths = new List<string>();
            currentPath = -1;

            this.pathAnalyzer = pathAnalyzer;
        }

        public String changePath(int change)
        {
            currentPath += change;
            String path =  getCurrentPath();
            pathAnalyzer.analyzePath(path);
            return path;
        }
        private void checkPathLength()
        {
            if (currentPath < 0)
                currentPath = 0;
            if (currentPath >= paths.Count)
            {
                paths.Add(pathAnalyzer.getPath());
                checkPathLength();
            }
        }
        public CSVRecord getCurrentRecord()
        {
            return pathAnalyzer.CSVRecord;
        }
        public void deleteLoadedRecord()
        {
            pathAnalyzer.deleteLoadedRecord();
        }
        public String getCurrentPath()
        {
            checkPathLength();
            return paths[currentPath];
        }
    }
}
