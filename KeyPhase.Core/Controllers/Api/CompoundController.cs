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

        // GET: api/Projects
        [HttpGet("CreateDefaultLayout")]
        public ProjectDetailed CreateDefaultLayout([FromQuery]int ProjectID)
        {
            return _combinedService.CreateDefaultLayout(ProjectID);
        }

        [HttpGet("CreateDefaultCoreLayout")]
        public ProjectOverview CreateDefaultCoreLayout(int UserID)
        {
            return _combinedService.CreateDefaultCoreLayout(UserID);
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

        [HttpPost("AddProject")]
        public ProjectOverview AddProject(int UserID, string Name, DateTime EstStartDT, DateTime EstEndDT, int PhaseID, double? Budget)
        {
            return _combinedService.AddProject(UserID, Name, EstStartDT, EstEndDT, PhaseID, Budget);
        }

        [HttpPost("AddTask")]
        public ProjectDetailed AddTask(int UserID, string Name, DateTime EstStartDT, DateTime EstEndDT,
            int PhaseID, int ProjectID, double? Cost)
        {
            return _combinedService.AddTask(UserID, Name, EstStartDT, EstEndDT, PhaseID, ProjectID, Cost);
        }

        [HttpGet("GetTaskPerProject")]
        public List<DashTaskPerProject> GetTaskPerProject(int UserID)
        {
            return _combinedService.GetTasksPerProject(UserID);
        }
    }
}