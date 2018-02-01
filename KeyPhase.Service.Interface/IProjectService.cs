using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Service.Interface
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
    }
}
