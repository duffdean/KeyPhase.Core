using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.DTO
{
    public class ReportingDataProject
    {
        public string Projects { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double MinCost { get; set; }
        public double MaxCost { get; set; }
        public bool Overdue { get; set; }
        public int DueIn { get; set; }
    }
}
