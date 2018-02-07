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
        IEnumerable<Project> GetAllForUser(int UserID);
    }
}
