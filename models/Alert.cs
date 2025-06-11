using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Malshinon.models
{
    public class Alert
    {
        public int Id { get; set; }
        public Target Target { get; set; }
        public DateTime TriggerTime { get; set; }
        public string Reason { get; set; }
    }
}


