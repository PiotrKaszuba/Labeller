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

        public String executablePath;
        public String codePath;
        public String args;
        public PathProvider(String executablePath = null, String codePath = null, String args = null)
        {
            if (executablePath == null) executablePath = PropertiesReader.PATH_PROVIDER_EXECUTABLE_PATH;
            if (codePath == null) codePath = Directory.GetCurrentDirectory() + PropertiesReader.PATH_PROVIDER_CODE_RELATIVE_PATH;
            if (args == null) args = PropertiesReader.PATH_PROVIDER_ADDITIONAL_ARGS;
            this.executablePath = executablePath;
            this.codePath = codePath;
            this.args = args;
        }
       
     

    
        private String run_cmd(string executable, string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = executable;
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                 
                    return result;
                }
            }
        }
        private String formatOutput(String path)
        {
            return path.Replace("\r\n", "");
        }
        public String getPath()
        {
            return formatOutput(run_cmd(this.executablePath, this.codePath, this.args));
        }



    }
}
