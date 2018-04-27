using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.DTO.Reports
{
    public class OverdueReport
    {
        public string TaskName { get; set; }
        public int DaysOverdue { get; set; }
    }
}
