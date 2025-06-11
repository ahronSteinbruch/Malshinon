using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Malshinon.models
{
    public class ReportTarget
    {
        public int ReportId { get; set; }
        public Report Report { get; set; }

        public int TargetId { get; set; }
        public Target Target { get; set; }
    }
}


