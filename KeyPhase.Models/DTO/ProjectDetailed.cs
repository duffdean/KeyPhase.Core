using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.DTO
{
    public class ProjectDetailed
    {
        public Project Project { get; set; }
        public List<Phase>PhaseTasks { get; set; }
    }
}
