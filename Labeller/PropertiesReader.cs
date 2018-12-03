using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labeller
{
    class PropertiesReader
    {
        public static String IMAGE_EXTENSION;
        public static String PATH_PROVIDER_EXECUTABLE_PATH;
        public static String PATH_PROVIDER_ADDITIONAL_ARGS;
        public static String PATH_PROVIDER_CODE_RELATIVE_PATH;
        public static String OUTPUT_CSV_RELATIVE_PATH;
        public static String PATIENT_DATA_RELATIVE_PATH;

        private static String formatParam(String paramLine)
        {
            return paramLine.Replace("\t", "");
        }
        public static void loadValues()
        {
            String[] lines = File.ReadAllLines("Properties.txt");
            Dictionary<String, String> dict = new Dictionary<string, string>();
            foreach (var item in lines)
            {
                try
                {
                    String[] split = formatParam(item).Split('=');
                    if(split.Length ==2)
                        dict[split[0]] = split[1];
                }
                catch(Exception e)
                {
                    MessageBox.Show("Błąd w ładowaniu parametru w linii: " + item);
                }
            }
            try
            {
                IMAGE_EXTENSION = dict["IMAGE_EXTENSION"];
                PATH_PROVIDER_EXECUTABLE_PATH = dict["PATH_PROVIDER_EXECUTABLE_PATH"];
                PATH_PROVIDER_ADDITIONAL_ARGS = dict["PATH_PROVIDER_ADDITIONAL_ARGS"];
                PATH_PROVIDER_CODE_RELATIVE_PATH = dict["PATH_PROVIDER_CODE_RELATIVE_PATH"];
                OUTPUT_CSV_RELATIVE_PATH = dict["OUTPUT_CSV_RELATIVE_PATH"];
                PATIENT_DATA_RELATIVE_PATH = dict["PATIENT_DATA_RELATIVE_PATH"];
            }
            catch(Exception e)
            {
                MessageBox.Show("Błąd w odczycie parametru");
            }
        }
        
    }
}
