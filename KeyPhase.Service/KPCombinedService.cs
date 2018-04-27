using KeyPhase.Models.DTO;
using KeyPhase.Models.DTO.Reports;
using KeyPhase.Models.Models;
using KeyPhase.Repository.Interface;
using KeyPhase.Service.Interface;
using Newtonsoft.Json;
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
        private readonly IRepository<Report> _reportRepository;
        private readonly IRepository<UserReport> _userReportRepository;


        public KPCombinedService(IRepository<Task> TaskRepository, IRepository<Project> ProjRepository, 
            IRepository<ProjectTask> ProjTaskRepository, IRepository<UserProject> userProjRepository, 
            IRepository<Phase> PhaseRepository, IRepository<ProjectTaskPhase> ProjTaskPhaseRepository,
            IRepository<ProjectHistory> ProjHistoryRepository, IRepository<TaskHistory> TaskHistoryRepository,
            IRepository<User> UserRepository, IRepository<PhaseUser> PhaseUserRepository, IRepository<UserTask> UserTaskRepository,
            IRepository<Report> ReportRepository, IRepository<UserReport> UserReportRepository)
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
            _reportRepository = ReportRepository;
            _userReportRepository = UserReportRepository;
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

        public ChartData GetTaskReportingData (int[] Tasks, DateTime StartDate, DateTime EndDate, double MinCost, 
            double MaxCost, bool Overdue, int DueIn, string ReportName, int UserID)
        {
            DateTime DueDate = new DateTime();
            List<Task> tempData = new List<Task>();
            ChartData reportData = new ChartData();

            reportData.Title = ReportName;
            reportData.YLabel = "Task Name";
            reportData.SeriesData = new List<ChartSeries>();

            List<Task> tasks = _taskRepository.GetAll().Where(p => Tasks.Any(cb => cb == p.ID)).ToList();

            DueDate.AddDays(DueIn);

            if (DueIn == 0)
            {
                DueDate = DateTime.Now;
            }
            
            tempData.AddRange(tasks);

            if (StartDate != DateTime.MinValue || EndDate != DateTime.MinValue)
            {
                tempData = tempData
                   .Where(t => t.ActEndDate <= EndDate)
                   .Where(t => t.ActStartDate >= StartDate).ToList();
            }

            if(MinCost >= 0)
            {
                if (MaxCost >= 1)
                {
                    tempData = tempData
                        .Where(t => t.Cost >= MinCost)
                        .Where(t => t.Cost <= MaxCost).ToList();
                }
                else
                {
                    tempData = tempData
                        .Where(t => t.Cost >= MinCost).ToList();
                }
                
            }

            //Create and overdue report
            if (Overdue)
            {
                tempData.Where(t => t.ActEndDate < DateTime.Now).ToList();
                //                public string TaskName { get; set; }
                //public int DaysOverdue { get; set; }
                //Create overdue report showing tasks due in x days
                if (DueIn > 0)
                {
                    tempData.Where(t => t.ActStartDate < DueDate).ToList();
                }

                //Create a cost report
                if (MinCost > 0 || MaxCost > 0)
                {

                }
                // or create overdue report
                else
                {
                    foreach (Task task in tempData)
                    {
                        reportData.XLabel = "Days Overdue";
                        reportData.SeriesData.Add(new ChartSeries() {
                            XSeries = (DueDate - DateTime.Parse(task.ActEndDate.ToString())).Days,
                            YSeries = task.Name
                        });
                    }
                }
            }
            else
            {
                if (DueIn > 0)
                {
                    tempData.Where(t => t.ActStartDate < DueDate).ToList();
                }

                //Create a cost report
                if(MinCost >= 0 || MaxCost >=0)
                {
                    reportData.XLabel = "Task Cost";

                    foreach (Task task in tempData)
                    {
                        reportData.SeriesData.Add(new ChartSeries()
                        {
                            XSeries = Convert.ToInt32(task.Cost),
                            YSeries = task.Name
                        });
                    }
                }
            }

            Report report = _reportRepository.Add(new Report()
            {
                Active = true,
                Data = JsonConvert.SerializeObject(reportData),
                Name = ReportName,
                CreatedOn = DateTime.Now
            });

            _userReportRepository.Add(new UserReport()
            {
                ReportID = report.ID,
                UserID = UserID
            });

            return reportData;
        }
        public ReportingData GetProjectReportingData(ReportingDataProject ProjData) { return null; }

        public List<ReportOverview> GetReportingDataOverview(int UserID)
        {
            List<UserReport> userReports;
            List<Report> reports;
            List<ReportOverview> overview = new List<ReportOverview>();

            userReports = _userReportRepository.FindAll(p => p.UserID == UserID).ToList();
            reports = _reportRepository.GetAll().Where(p => userReports.Any(cb => cb.ReportID == p.ID)).ToList();

            for (int i = reports.Count; i-- > 0;)
            {
                overview.Add(new ReportOverview()
                {
                    ID = reports[i].ID,
                    CreatedOn = DateTime.Parse(reports[i].CreatedOn.ToString()),
                    Name = reports[i].Name
                });
            }

            //foreach (Report report in reports)
            //{
            //    overview.Add(new ReportOverview()
            //    {
            //        ID = report.ID,
            //        CreatedOn = DateTime.Parse(report.CreatedOn.ToString()),
            //        Name = report.Name
            //    });
            //}
            return overview.OrderByDescending(t => t.CreatedOn).ToList();
        }

        public ChartData GetReportByID(int ReportID)
        {
            Report report = _reportRepository.Get(ReportID);

            return JsonConvert.DeserializeObject<ChartData>(report.Data);
        }

        public void TaskPhaseHistory(int PrevPhase, int CurrPhase, int TaskID, int UserID)
        {
            Phase prevPhase = _phaseRepository.Get(PrevPhase);
            Phase currPhase = _phaseRepository.Get(CurrPhase);

            _taskHistoryRepository.Add(new TaskHistory()
            {
                Active = true,
                DateSubmitted = DateTime.Now,
                TaskID = TaskID,
                UserID = UserID,
                Value = "Task moved from " + prevPhase.Name + " to "  + currPhase.Name + "." 
            });
        }
    }
}