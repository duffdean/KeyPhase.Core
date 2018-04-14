using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KeyPhase.Service.Interface
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        List<Project> GetAllForUser(int UserID);
        List<Project> GetAllForTeam(int UserID);
    }
}
