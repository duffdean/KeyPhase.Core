using KeyPhase.Models.DTO;
using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KeyPhase.Service.Interface
{
    public interface IKPCombinedService
    {
        ProjectDetailed SelectedProject(int ProjectID);
        ProjectOverview UserProjectsOverview(int UserID);
        TaskDetailed TaskDetailed(int TaskID);
        TaskHistory AddTaskHistory(int TaskID, int UserID, string Value);
        ProjectDetailed CreateDefaultLayout(int ProjectID);
        bool AddProject(int UserID, string Name, DateTime EstStartDT, DateTime EstEndDT, int PhaseID);
        //IEnumerable<Task> GetAll();
        //IEnumerable<Task> GetAllForProject(int ProjectID);
    }
}