using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labeller
{
    class Utils
    {
        private static List<int> getListFromRecord(String record)
        {
            List<int> list = new List<int>();
            if (record != null)
            {
                String[] vals = record.Split('-');
                foreach (var item in vals)
                {
                    try
                    {
                        list.Add(Int32.Parse(item));
                    }
                    catch(Exception e)
                    {

                    }
                }
            }
            return list;
        }
        private static String getRecordFromList(List<int> list)
        {
            return String.Join("-", list);
        }
        public static void initAutoMapper()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<CSVStructure, CSVRecord>()
                    .ForMember(o => o.PierscienR, opt =>
                        opt.MapFrom(src =>
                            getRecordFromList(src.PierscienR)))
                     .ForMember(o => o.PierscienStartR, opt =>
                        opt.MapFrom(src =>
                            getRecordFromList(src.PierscienStartR)))
                     .ForMember(o => o.PierscienStartAngle, opt =>
                        opt.MapFrom(src =>
                            getRecordFromList(src.PierscienStartAngle)))
                     .ForMember(o => o.PierscienStopAngle, opt =>
                        opt.MapFrom(src =>
                            getRecordFromList(src.PierscienStopAngle)))
                     .ForMember(o => o.Eye, opt =>
                        opt.MapFrom(src =>
                            getEyeName(src.eye)))
                .ReverseMap()
                    .ForMember(o => o.currentAngle, opt => opt.Ignore())
                    .ForMember(o => o.PierscienR, opt =>
                        opt.MapFrom(src =>
                             getListFromRecord(src.PierscienR)))
                    .ForMember(o => o.PierscienStartR, opt =>
                        opt.MapFrom(src =>
                             getListFromRecord(src.PierscienStartR)))
                    .ForMember(o => o.PierscienStartAngle, opt =>
                        opt.MapFrom(src =>
                             getListFromRecord(src.PierscienStartAngle)))
                    .ForMember(o => o.PierscienStopAngle, opt =>
                        opt.MapFrom(src =>
                             getListFromRecord(src.PierscienStopAngle)))
                     .ForMember(o => o.eye, opt =>
                        opt.MapFrom(src =>
                            reverseEyeName(src.Eye)));

            });
        }
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


        public static int GetNewVal(int val, int increment, int maxVal = 999, int minVal = -1)
        {
            return val + increment > maxVal ? minVal : val + increment < minVal ? maxVal : val + increment;
        }
        public static String getEyeName(int eye)
        {
            return eye == 0 ? "left_eye_images" : "right_eye_images";
        }
        public static int reverseEyeName(String eyeName)
        {
            if (eyeName.Contains("left")) return 0;
            else return 1;
        }
    }
}
