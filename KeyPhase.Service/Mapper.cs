using AutoMapper;
using KeyPhase.Models.DTO;
using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Service
{
    public static class Mapper
    {
        public static ProjectDetailed MapCustomerDetails(Project project, List<Phase> phases)
        {
            ProjectDetailed projDetailed = new ProjectDetailed();

            phases.Sort((x, y) => x.Position.CompareTo(y.Position));

            projDetailed.Project = project;
            projDetailed.PhaseTasks = phases;            

            return projDetailed;
        }
    }
}
