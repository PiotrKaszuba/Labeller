using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labeller
{
    public class CSVRecord
    {
        public String Patient { get; set; }
        public String Date { get; set; }
        public String Eye { get; set; }
        public String EyeImage { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int SrodekX { get; set; }
        public int SrodekY { get; set; }
        public int SrodekR { get; set; }
        public String SrodekImage { get; set; }

        public int WyjscieX { get; set; }
        public int WyjscieY { get; set; }
        public int WyjscieR { get; set; }
        public String WyjscieImage { get; set; }

        public int Krawedz { get; set; }
        public String KrawedzImage { get; set; }

        public int Obrzek { get; set; }
        public String ObrzekImage { get; set; }

        public int PierscienX { get; set; }
        public int PierscienY { get; set; }
        public int PierscienR { get; set; }
        public int PierscienStartR { get; set; }
        public String PierscienStartAngle { get; set; }
        public String PierscienStopAngle { get; set; }
        public String PierscienImage { get; set; }
    }
}
