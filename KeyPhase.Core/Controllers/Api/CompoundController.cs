using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyPhase.Models.DTO;
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
        [HttpGet]
        public ProjectDetailed GetProjectData(int ProjectID)
        {
            return _combinedService.SelectedProject(ProjectID);
        }
    }
}