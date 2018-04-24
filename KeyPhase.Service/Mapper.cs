using AutoMapper;
using KeyPhase.Models.DTO;
using KeyPhase.Models.DTO.Dash;
using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Service
{
    public static class Mapper
    {
        public static ProjectDetailed MapCustomerDetails(Project project, List<Phase> phases)
        {
            ProjectDetailed projDetailed = new ProjectDetailed();

            phases.Sort((x, y) => x.Position.CompareTo(y.Position));

            projDetailed.Project = project;
            projDetailed.PhaseTasks = phases;

            return projDetailed;
        }

        public static ProjectDetailed MapCorePhases(IEnumerable<Project> projects, List<Phase> phases)
        {
            //ProjectOverview projOverview = new ProjectOverview();

            //phases.Sort((x, y) => x.Position.CompareTo(y.Position));

            //projOverview.Projects = projects;
            //projOverview.ProjectPhases = phases;

            //return projOverview;
            return null;
        }

        public static ProjectOverview MapProjectOverview(List<Project> projects, List<Phase> phases)
        {
            ProjectOverview projOverview = new ProjectOverview();

            phases.Sort((x, y) => x.Position.CompareTo(y.Position));

            projOverview.Projects = projects;
            projOverview.ProjectPhases = phases;

            return projOverview;
        }

        public static TaskDetailed MapTaskDetailed(Task task, List<TaskHistory> taskHistory)
        {
            return new TaskDetailed()
            {
                Task = task,
                TaskHistory = taskHistory
            };
        }

        public static List<DashTaskPerProject> GetTasksPerProject(IEnumerable<Project> Projects, List<ProjectTask> ProjectTasks)
        {
            List<DashTaskPerProject> dttp = new List<DashTaskPerProject>();
            string projName = "";
            int tasks = 0;

            foreach (Project p in Projects)
            {
                projName = p.Name;
                tasks = 0;

                foreach (ProjectTask pt in ProjectTasks)
                {
                    if (pt.ProjectID == p.ID)
                    {
                        tasks++;
                    }
                }

                dttp.Add(new DashTaskPerProject()
                {
                    ProjectName = projName,
                    TotalTasks = tasks
                });
            }

            return dttp;
        }

        public static List<DashMostRecentTasks> MapRecentTasks(List<Project> Projects, List<ProjectTask> ProjTasks, IEnumerable<Task> Tasks)
        {
            List<DashMostRecentTasks> recentTasks = new List<DashMostRecentTasks>();

            foreach (Task t in Tasks)
            {
                foreach (ProjectTask pt in ProjTasks)
                {
                    if (pt.TaskID == t.ID)
                    {
                        foreach (Project p in Projects)
                        {
                            if (pt.ProjectID == p.ID)
                            {
                                recentTasks.Add(new DashMostRecentTasks()
                                {
                                    TaskName = t.Name,
                                    ProjectName = p.Name,
                                    Cost = t.Cost
                                });
                            }
                        }
                    }

                }
            }
            return recentTasks;
        }

        public static List<DashActiveVsComplete> MapActiveVsComplete(List<Task> Tasks)
        {
            List<DashActiveVsComplete> avc = new List<DashActiveVsComplete>();
            DashActiveVsComplete complete = new DashActiveVsComplete() { Series = "Complete", Total = 0 };
            DashActiveVsComplete active = new DashActiveVsComplete() { Series = "Active", Total = 0 };

            foreach (Task t in Tasks)
            {
                if ((bool)t.Complete)
                {
                    complete.Total++;
                }
                else
                {
                    active.Total++;
                }
            }

            avc.Add(complete);
            avc.Add(active);

            return avc;
        }

        public static List<DashOverdueTasks> MapOverdueTasks(List<Task> Tasks)
        {
            List<DashOverdueTasks> OverdueTasks = new List<DashOverdueTasks>();
            DateTime taskDate;
            DateTime todayDate = DateTime.Now;

            foreach (Task task in Tasks)
            {

                if (task.ActEndDate != null)
                {
                    taskDate = Convert.ToDateTime(task.ActEndDate);

                    OverdueTasks.Add(new DashOverdueTasks()
                    {
                        DaysOverdue = (taskDate - todayDate).Days,
                        TaskName = task.Name
                    });
                }
                else if (task.EstEndDate != null)
                {
                    taskDate = Convert.ToDateTime(task.EstEndDate);

                    OverdueTasks.Add(new DashOverdueTasks()
                    {
                        DaysOverdue = (todayDate - taskDate).Days,
                        TaskName = task.Name
                    });
                }
            }

            return OverdueTasks;
        }

        public static ReportingData MapReportingData(List<Project> Projects, List<Task> Tasks)
        {
            ReportingData data = new ReportingData() {
                Projects = new List<BasicItem>(),
                Tasks = new List<BasicItem>()
            };

            foreach (Project proj in Projects)
            {
                data.Projects.Add(new BasicItem() {
                    ID = proj.ID,
                    Name = proj.Name
                });
            }

            foreach (Task task in Tasks)
            {
                data.Tasks.Add(new BasicItem()
                {
                    ID = task.ID,
                    Name = task.Name
                });
            }

            return data;
        }
    }
}
