using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labeller
{
    public class PatientRecord
    {
        public String md5 { get; set; }
        public String sex { get; set; }
        public String age { get; set; }
        public String examination_date { get; set; }
        public String correct_icd_code { get; set; }
        public String description1 { get; set; }
        public String description2 { get; set; }
        public String description3 { get; set; }
        public String description4 { get; set; }
    }
}
