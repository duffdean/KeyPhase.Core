using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.DTO
{
    public class ReportingData
    {
        public List<BasicItem> Projects { get; set; }
        public List<BasicItem> Tasks { get; set; }
    }
}
