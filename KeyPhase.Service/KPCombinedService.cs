using KeyPhase.Models.DTO;
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
        private readonly IRepository<Project> _projRepository;
        private readonly IRepository<UserProject> _userProjectRepository;
        private readonly IRepository<Phase> _phaseRepository;

        public KPCombinedService(IRepository<Task> TaskRepository, IRepository<Project> ProjRepository, 
            IRepository<ProjectTask> ProjTaskRepository, IRepository<UserProject> userProjRepository, 
            IRepository<Phase> PhaseRepository)
        {
            _taskRepository = TaskRepository;
            _projTaskRepository = ProjTaskRepository;
            _phaseRepository = PhaseRepository;
            _projRepository = ProjRepository;
            _userProjectRepository = userProjRepository;
        }

        public ProjectDetailed SelectedProject(int ProjectID)
        {
            Project project = _projRepository.Get(ProjectID);
            IEnumerable<ProjectTask> projTasks = _projTaskRepository.FindAll(c => c.ProjectID == project.ID);
            List<Task> tasks = _taskRepository.GetAll().Where(t => projTasks.Any(cb => cb.TaskID == t.ID)).ToList();
            List<Phase> phases = _phaseRepository.GetAll().Where(t => tasks.Any(cb => cb.PhaseID == t.ID)).ToList();

            return Mapper.MapCustomerDetails(project, phases);
        }

        public ProjectOverview UserProjectsOverview(int UserID)
        {
            IEnumerable<UserProject> userProjects = _userProjectRepository.FindAll(c => c.UserID == UserID);
            List<Project> projects = _projRepository.GetAll().Where(p => userProjects.Any(cb => cb.ProjectID == p.ID)).ToList();
            List<Phase> phases = _phaseRepository.GetAll().Where(t => projects.Any(cb => cb.PhaseID == t.ID)).ToList();

            return Mapper.MapProjectOverview(projects, phases);
        }
    }
}