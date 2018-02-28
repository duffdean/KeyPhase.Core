using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KeyPhase.Models;
using KeyPhase.Models.Models;
using KeyPhase.Service.Interface;

namespace KeyPhase.Core.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService TaskService)
        {
            _taskService = TaskService;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<KeyPhase.Models.Models.Task> GetProjectTasks(int ProjectID)
        {
            return _taskService.GetAllForProject(ProjectID);
        }
        
        [HttpPost("UpdateTaskPhase")]
        public void UpdateTaskPhase(int PhaseID, int TaskID)
        {
            KeyPhase.Models.Models.Task task = _taskService.GetTask(TaskID);
            task.PhaseID = PhaseID;

            _taskService.UpdateTaskPhase(PhaseID, TaskID);
        }
    }
}