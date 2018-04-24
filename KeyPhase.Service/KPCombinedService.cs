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
        private readonly IRepository<PhaseUser> _phaseUserRepository;
        private readonly IRepository<UserTask> _userTaskRepository;

        public KPCombinedService(IRepository<Task> TaskRepository, IRepository<Project> ProjRepository, 
            IRepository<ProjectTask> ProjTaskRepository, IRepository<UserProject> userProjRepository, 
            IRepository<Phase> PhaseRepository, IRepository<ProjectTaskPhase> ProjTaskPhaseRepository,
            IRepository<ProjectHistory> ProjHistoryRepository, IRepository<TaskHistory> TaskHistoryRepository,
            IRepository<User> UserRepository, IRepository<PhaseUser> PhaseUserRepository, IRepository<UserTask> UserTaskRepository)
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
            _phaseUserRepository = PhaseUserRepository;
            _userTaskRepository = UserTaskRepository;
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

        public ProjectDetailed CreateDefaultLayout(int ProjectID)
        {
            Phase phase; 

            string[] phases = new string[] {
                "New",
                "In Progress",
                "Complete",
                "On-Hold"
            };

            for (int i = 0; i < phases.Length; i++)
            {
                phase = _phaseRepository.Add(new Phase
                {
                    Name = phases[i],
                    Position = i + 1,
                    Active = true
                });

                _projTaskPhaseRepository.Add(new ProjectTaskPhase
                {
                    ProjectID = ProjectID,
                    PhaseID = phase.ID,
                    Active = true
                });
            }

            //_phaseRepository.Add(new Phase
            //{
            //    Name = "New",
            //    Position = 1,
            //    Active = true
            //});

            //_phaseRepository.Add(new Phase
            //{
            //    Name = "In Progress",
            //    Position = 2,
            //    Active = true
            //});

            //_phaseRepository.Add(new Phase
            //{
            //    Name = "Complete",
            //    Position = 3,
            //    Active = true
            //});

            //_phaseRepository.Add(new Phase
            //{
            //    Name = "On-Hold",
            //    Position = 4,
            //    Active = true
            //});

            //Project project = _projRepository.Get(ProjectID);
            //List<ProjectTaskPhase> taskPhases = _projTaskPhaseRepository.FindAll(p => p.ProjectID == ProjectID).ToList();
            //IEnumerable<ProjectTask> projTasks = _projTaskRepository.FindAll(c => c.ProjectID == project.ID);
            //List<Task> tasks = _taskRepository.GetAll().Where(t => projTasks.Any(cb => cb.TaskID == t.ID)).ToList();
            //List<Phase> phases = _phaseRepository.GetAll().Where(t => taskPhases.Any(cb => cb.PhaseID == t.ID)).ToList();

            //return Mapper.MapCustomerDetails(project, phases);

            return SelectedProject(ProjectID);
        }

        public ProjectOverview CreateDefaultCoreLayout(int UserID)
        {
            Phase phase;

            string[] phases = new string[] {
                "New",
                "In Progress",
                "Complete",
                "On-Hold"
            };

            for (int i = 0; i < phases.Length; i++)
            {
                phase = _phaseRepository.Add(new Phase
                {
                    Name = phases[i],
                    Position = i + 1,
                    Active = true
                });

                _phaseUserRepository.Add(new PhaseUser
                {
                    UserID = UserID,
                    PhaseID = phase.ID,
                });
            }

            return UserProjectsOverview(UserID);
        }

        public ProjectOverview UserProjectsOverview(int UserID)
        {
            //IEnumerable<UserProject> userProjects = _userProjectRepository.FindAll(c => c.UserID == UserID);            
            //List<Project> projects = _projRepository.GetAll().Where(p => userProjects.Any(cb => cb.ProjectID == p.ID)).ToList();
            //List<Phase> phases = _phaseRepository.GetAll().Where(t => projects.Any(cb => cb.PhaseID == t.ID)).ToList();

            IEnumerable<UserProject> userProjects = _userProjectRepository.FindAll(c => c.UserID == UserID);
            List<Project> projects = _projRepository.GetAll().Where(p => userProjects.Any(cb => cb.ProjectID == p.ID)).ToList();
            List<PhaseUser> userPhases = _phaseUserRepository.FindAll(p => p.UserID == UserID).ToList();
            List<Phase> phases = _phaseRepository.GetAll().Where(t => userPhases.Any(cb => cb.PhaseID == t.ID)).ToList();

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

        public ProjectOverview AddProject(int UserID, string Name, DateTime EstStartDT, DateTime EstEndDT, int PhaseID, double? Budget)
        {
            Project proj;

            proj = _projRepository.Add(new Project
            {
                Name = Name,
                PhaseID = PhaseID,
                EstStartDate = EstStartDT,
                EstEndDate = EstEndDT,
                Budget = Budget,
                Active = true
            });

            _userProjectRepository.Add(new UserProject
            {
                ProjectID = proj.ID,
                UserID = UserID
            });


            return UserProjectsOverview(UserID);
        }

        public ProjectDetailed AddTask(int UserID, string Name, DateTime EstStartDT, DateTime EstEndDT, 
            int PhaseID, int ProjectID, double? Cost)
        {
            Task task;

            task = _taskRepository.Add(new Task
            {
                Name = Name,
                PhaseID = PhaseID,
                EstStartDate = EstStartDT,
                EstEndDate = EstEndDT,
                Cost = Cost,
                Complete = false,
                CreatedOn = DateTime.Now,
                Active = true
            });

            _projTaskRepository.Add(new ProjectTask
            {
                ProjectID = ProjectID,
                TaskID = task.ID
            });

            _userTaskRepository.Add(new UserTask
            {
                UserID = UserID,
                TaskID = task.ID
            });

            AddTaskHistory(task.ID, UserID, task.Name + " was created");

            return SelectedProject(ProjectID);
        }

        public List<DashTaskPerProject> GetTasksPerProject(int UserID)
        {
            IEnumerable<UserProject> userProjects = _userProjectRepository.FindAll(c => c.UserID == UserID);
            List<Project> projects = _projRepository.GetAll().Where(p => userProjects.Any(cb => cb.ProjectID == p.ID)).ToList();
            List<ProjectTask> projTasks = _projTaskRepository.GetAll().Where(t => projects.Any(ut => ut.ID == t.ProjectID)).ToList();

            return Mapper.GetTasksPerProject(projects, projTasks);
        }

        public ReportingData GetReportingData(int UserID)
        {
            IEnumerable<UserProject> userProjects = _userProjectRepository.FindAll(c => c.UserID == UserID);
            List<Project> projects = _projRepository.GetAll().Where(p => userProjects.Any(cb => cb.ProjectID == p.ID)).ToList();
            List<ProjectTask> projTasks = _projTaskRepository.GetAll().Where(t => projects.Any(ut => ut.ID == t.ProjectID)).ToList();
            List<Task> tasks = _taskRepository.GetAll().Where(p => projTasks.Any(cb => cb.TaskID == p.ID)).ToList();

            return Mapper.MapReportingData(projects, tasks);
        }

        public ReportingData GetProjectReportingData(ReportingDataTask TaskData) { return null; }
        public ReportingData GetTaskReportingData(ReportingDataProject ProjData) { return null; }
        public ReportingData GetReportingDataOverview(int UserID) { return null; }
    }
}