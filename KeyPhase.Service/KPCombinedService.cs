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
        private readonly IRepository<ProjectTaskPhase> _projTaskPhaseRepository;
        private readonly IRepository<ProjectHistory> _projHistoryRepository;
        private readonly IRepository<TaskHistory> _taskHistoryRepository;
        private readonly IRepository<User> _userRepository;

        public KPCombinedService(IRepository<Task> TaskRepository, IRepository<Project> ProjRepository, 
            IRepository<ProjectTask> ProjTaskRepository, IRepository<UserProject> userProjRepository, 
            IRepository<Phase> PhaseRepository, IRepository<ProjectTaskPhase> ProjTaskPhaseRepository,
            IRepository<ProjectHistory> ProjHistoryRepository, IRepository<TaskHistory> TaskHistoryRepository,
            IRepository<User> UserRepository)
        {
            _taskRepository = TaskRepository;
            _projTaskRepository = ProjTaskRepository;
            _phaseRepository = PhaseRepository;
            _projRepository = ProjRepository;
            _userProjectRepository = userProjRepository;
            _projTaskPhaseRepository = ProjTaskPhaseRepository;
            _projHistoryRepository = ProjHistoryRepository;
            _taskHistoryRepository = TaskHistoryRepository;
            _userRepository = UserRepository;
        }

        public ProjectDetailed SelectedProject(int ProjectID)
        {
            Project project = _projRepository.Get(ProjectID);
            List<ProjectTaskPhase> taskPhases = _projTaskPhaseRepository.FindAll(p => p.ProjectID == ProjectID).ToList();
            IEnumerable<ProjectTask> projTasks = _projTaskRepository.FindAll(c => c.ProjectID == project.ID);
            List<Task> tasks = _taskRepository.GetAll().Where(t => projTasks.Any(cb => cb.TaskID == t.ID)).ToList();
            List<Phase> phases = _phaseRepository.GetAll().Where(t => taskPhases.Any(cb => cb.PhaseID == t.ID)).ToList();

            return Mapper.MapCustomerDetails(project, phases);
        }

        public ProjectOverview UserProjectsOverview(int UserID)
        {
            IEnumerable<UserProject> userProjects = _userProjectRepository.FindAll(c => c.UserID == UserID);            
            List<Project> projects = _projRepository.GetAll().Where(p => userProjects.Any(cb => cb.ProjectID == p.ID)).ToList();
            List<Phase> phases = _phaseRepository.GetAll().Where(t => projects.Any(cb => cb.PhaseID == t.ID)).ToList();

            return Mapper.MapProjectOverview(projects, phases);
        }

        public TaskDetailed TaskDetailed(int TaskID)
        {
            Task task = _taskRepository.Get(TaskID);
            List<TaskHistory> taskHistory = _taskHistoryRepository.FindAll(t => t.TaskID == task.ID).ToList();

            foreach (TaskHistory th in taskHistory)
            {
                th.User = _userRepository.Get(th.UserID);
            }

            return Mapper.MapTaskDetailed(task, taskHistory);
        }

        public TaskHistory AddTaskHistory(int TaskID, int UserID, string Value)
        {
            return _taskHistoryRepository.Add(new TaskHistory()
            {
                UserID = UserID,
                Value = Value,
                TaskID = TaskID,
                DateSubmitted = DateTime.Now,
                Active = true
            });
        }
    }
}