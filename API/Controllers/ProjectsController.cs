using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Service;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {

        private readonly IProjectService _projectService = projectService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllAsync();
            if (projects == null)
                return NotFound();

            return Ok(projects);
        }

        [HttpGet]
        [Route("{ID:guid}")]
        public async Task<IActionResult> GetById(Guid ID)
        {
            var project = await _projectService.GetByIDAsync(ID);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateDTO projectCreateDTO)
        {

            var created = await _projectService.CreateAsync(projectCreateDTO);

            return CreatedAtAction(nameof(GetById),
                new { id = created.ID },
                created);

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid ID, ProjectUpdateDTO dto)
        {

            var updatedProject = await _projectService.UpdateAsync(ID, dto);

            if (updatedProject == null)
                return NotFound();

            return Ok(updatedProject);

        }

        [HttpDelete("{ID:guid}")]
        public async Task<IActionResult> Delete(Guid ID)
        {
           
            var success = await _projectService.DeleteAsync(ID);

            if (!success)
                return NotFound();

            return NoContent();

        }

    }

}
