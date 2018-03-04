using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyPhase.Models.DTO;
using KeyPhase.Models.Models;
using KeyPhase.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KeyPhase.Core.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Compound")]
    public class CompoundController : Controller
    {
        private readonly IKPCombinedService _combinedService;

        public CompoundController(IKPCombinedService CombinedService)
        {
            _combinedService = CombinedService;
        }

        // GET: api/Projects
        [HttpGet("GetProjectData")]
        public ProjectDetailed GetProjectData([FromQuery]int ProjectID)
        {
            return _combinedService.SelectedProject(ProjectID);
        }

        [HttpGet("GetProjectsOverview")]
        public ProjectOverview GetProjectsOverview(int UserID)
        {
            return _combinedService.UserProjectsOverview(UserID);
        }

        [HttpGet("GetTaskDetailed")]
        public TaskDetailed GetTaskDetailed(int TaskID)
        {
            return _combinedService.TaskDetailed(TaskID);
        }

        [HttpPost("AddTaskHistory")]
        public TaskHistory AddTaskHistory(int TaskID, int UserID, string Value)
        {
            return _combinedService.AddTaskHistory(TaskID, UserID, Value);
        }
    }
}