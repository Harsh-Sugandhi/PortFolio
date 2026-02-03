using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController(IProjectRepository projectRepository) : Controller
    {

        private readonly IProjectRepository _projectRepository = projectRepository;

        [HttpGet]
        public async Task<IActionResult> ProjectList()
        {
            var projects = await _projectRepository.FetchProjectListAsync();
            return Ok(projects);
        }

        [HttpGet]
        [Route("{ID:guid}")]
        public async Task<IActionResult> Project(Guid ID)
        {
            var project = await _projectRepository.FetchProjectByIDAsync(ID);
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project)
        {
            await _projectRepository.AddProjectAsync(project);
            return Ok(project);
        }

    }

}
