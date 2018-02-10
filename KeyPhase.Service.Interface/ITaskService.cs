using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KeyPhase.Service.Interface
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAll();
        IEnumerable<Task> GetAllForProject(int ProjectID);
    }
}