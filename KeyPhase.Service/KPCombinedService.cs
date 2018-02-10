using KeyPhase.Models.Models;
using KeyPhase.Repository.Interface;
using KeyPhase.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KeyPhase.Service
{
    public class KPCombinedService : IKPCombinedService
    {
        private readonly IRepository<Task> _taskRepository;
        private readonly IRepository<ProjectTask> _projTaskRepository;

        public KPCombinedService(IRepository<Task> TaskRepository, IRepository<Project> ProjRepository, 
            IRepository<ProjectTask> ProjTaskRepository, IRepository<UserProject> userProjRepository)
        {
            _taskRepository = TaskRepository;
            _projTaskRepository = ProjTaskRepository;
        }
    }
}