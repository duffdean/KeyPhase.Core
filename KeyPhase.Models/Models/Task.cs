using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.Models
{
    public class Task
    {
        public Task() { }

        public int ID { get; set; }
        public string Name { get; set; }
        public int? PhaseID { get; set; }
        public DateTime? EstStartDate { get; set; }
        public DateTime? EstEndDate { get; set; }
        public DateTime? ActStartDate { get; set; }
        public DateTime? ActEndDate { get; set; }
        public double? Cost { get; set; }
        public bool? Active { get; set; }
        public virtual Phase Phase { get; set; }
    }
}
