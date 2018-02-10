using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyPhase.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KeyPhase.Core.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/KPCombined")]
    public class KPCombinedController : Controller
    {
        private readonly ITaskService _taskService;

        public KPCombinedController(ITaskService TaskService)
        {
            _taskService = TaskService;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<KeyPhase.Models.Models.Task> GetProjectTasks(int ProjectID)
        {
            return _taskService.GetAllForProject(ProjectID);
        }
    }
}