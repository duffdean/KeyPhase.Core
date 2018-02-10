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
    public class TaskService : ITaskService
    {
        private readonly IRepository<Task> _taskRepository;
        private readonly IRepository<ProjectTask> _projTaskRepository;

        public TaskService(IRepository<Task> TaskRepository, IRepository<ProjectTask> ProjTaskRepository)
        {
            _taskRepository = TaskRepository;
            _projTaskRepository = ProjTaskRepository;
        }

        public IEnumerable<Task> GetAll()
        {
            var a = _taskRepository.GetAll();

            return a;
        }

        public IEnumerable<Task> GetAllForProject(int ProjectID)
        {
            var b = _projTaskRepository.FindAll(c => c.ProjectID == ProjectID);
            //var a = _testRepository.FindAll(c => c.Name == "test project");
            IEnumerable<Task> a = _taskRepository.GetAll().Where(t => b.Any(cb => cb.TaskID == t.ID));

            return a;
        }
    }
}