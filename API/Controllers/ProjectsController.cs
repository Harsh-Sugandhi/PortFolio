using Core.DTOs.Projects;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {

        private readonly IProjectService _projectService = projectService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProjectQueryDTO query)
        {
            var result = await _projectService.GetAllAsync(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _projectService.GetByIDAsync(id);
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
        public async Task<IActionResult> Update(Guid id, ProjectUpdateDTO dto)
        {

            var updatedProject = await _projectService.UpdateAsync(id, dto);

            if (updatedProject == null)
                return NotFound();

            return Ok(updatedProject);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var success = await _projectService.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();

        }

    }

}
