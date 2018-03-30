using AutoMapper;
using KeyPhase.Models.DTO;
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
                    if(pt.ProjectID == p.ID)
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
    }
}
