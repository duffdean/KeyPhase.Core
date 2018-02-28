using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KeyPhase.Service.Interface
{
    public interface ITaskService
    {
        Task GetTask(int TaskID);
        IEnumerable<Task> GetAll();
        IEnumerable<Task> GetAllForProject(int ProjectID);
        void UpdateTaskPhase(int PhaseID, int TaskID);
    }
}