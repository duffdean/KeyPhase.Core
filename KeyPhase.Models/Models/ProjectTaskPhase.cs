using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.Models
{
    public class ProjectTaskPhase
    {
        public int ProjectID { get; set; }
        public int PhaseID { get; set; }
        public bool Active { get; set; }
    }
}
