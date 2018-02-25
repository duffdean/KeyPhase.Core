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
        //IEnumerable<Task> GetAll();
        //IEnumerable<Task> GetAllForProject(int ProjectID);
    }
}