using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labeller
{
    public class PathProvider
    {
        public List<String> paths;
        public int currentPath;
        public PathProvider()
        {
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
            start.FileName = "C:/Users/Piotr/AppData/Local/Programs/Python/Python36/python.exe";
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

        private String getPath()
        {
            return run_cmd(Directory.GetCurrentDirectory()+"/template.py", "");
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
