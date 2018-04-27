using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.Models
{
    public class Report
    {
        public Report() { }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
