using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.DTO
{
    public class ProjectOverview
    {
        public List<Project> Projects { get; set; }
        public List<Phase> ProjectPhases { get; set; }
    }
}
