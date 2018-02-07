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
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService ProjectService)
        {
            _projectService = ProjectService;
        }

        // GET: api/Projects
        [HttpGet]
        public IEnumerable<Project> GetUserProjects(int UserID)
        {
            return _projectService.GetAllForUser(UserID);
        }

        //// GET: api/Projects/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProject([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var project = await _context.Project.SingleOrDefaultAsync(m => m.ID == id);

        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(project);
        //}

        //// PUT: api/Projects/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProject([FromRoute] int id, [FromBody] Project project)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != project.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(project).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProjectExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Projects
        //[HttpPost]
        //public async Task<IActionResult> PostProject([FromBody] Project project)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Project.Add(project);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProject", new { id = project.ID }, project);
        //}

        //// DELETE: api/Projects/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProject([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var project = await _context.Project.SingleOrDefaultAsync(m => m.ID == id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Project.Remove(project);
        //    await _context.SaveChangesAsync();

        //    return Ok(project);
        //}

        //private bool ProjectExists(int id)
        //{
        //    return _context.Project.Any(e => e.ID == id);
        //}
    }
}