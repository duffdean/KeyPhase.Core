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
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserTask> _userTaskRepository;

        public TaskService(IRepository<Task> TaskRepository, IRepository<ProjectTask> ProjTaskRepository,
            IRepository<User> UserRepository, IRepository<UserTask> UserTaskRepository)
        {
            _taskRepository = TaskRepository;
            _projTaskRepository = ProjTaskRepository;
            _userRepository = UserRepository;
            _userTaskRepository = UserTaskRepository;
        }

        public Task GetTask(int TaskID)
        {
            return _taskRepository.Get(TaskID);
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

        public void UpdateTaskPhase(int PhaseID, int TaskID)
        {
            Task task = _taskRepository.Get(TaskID);
            task.PhaseID = PhaseID;
            _taskRepository.Update(task, TaskID);
        }

        public IEnumerable<Task> GetMostRecent(int UserID)
        {
            var userTasks = _userTaskRepository.FindAll(c => c.UserID == UserID);
            IEnumerable<Task> tasks = _taskRepository.GetAll().Where(t => userTasks.Any(ut => ut.TaskID == t.ID));

            return tasks.OrderByDescending(t => t.CreatedOn).Take(10);
        }
    }
}