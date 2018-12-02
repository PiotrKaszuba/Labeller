using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labeller
{
    class Utils
    {

        private static String[] getDirs(string path)
        {
            String[] Dirs = path.Split('\\');
            if (Dirs.Length == 1)
                Dirs = path.Split('/');

            if (Dirs[Dirs.Length - 1].Equals(""))
            {
                String[] NewDirs = new string[Dirs.Length - 1];
                Array.Copy(Dirs, NewDirs, Dirs.Length - 1);
                return NewDirs;
            }
            return Dirs;
        }
        private static int getEyeOffset(string path)
        {
            if (path.Contains("eye_images"))
                return -1;
            else return 0;
        }
        public static String getPatient(string path)
        {
            String[] Dirs = getDirs(path);
            return Dirs[Dirs.Length - 2 + getEyeOffset(path)];
           
        }
        public static String getDate(string path)
        {
            String[] Dirs = getDirs(path);
            return Dirs[Dirs.Length - 1 + getEyeOffset(path)];
        }
        public static String getEye(string path, int eye)
        {
            String[] Dirs = getDirs(path);
            if (getEyeOffset(path) == 0) return getEyeName(eye);
            return Dirs[Dirs.Length - 1];
        }


        public static int GetNewVal(int val, int increment, int maxVal)
        {
            return val + increment > maxVal ? -1 : val + increment < 0 ? maxVal : val + increment;
        }
        public static String getEyeName(int eye)
        {
            return eye == 0 ? "left_eye_images" : "right_eye_images";
        }
    }
}
