using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Service;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class ProjectService(IProjectRepository repository, ILogger<ProjectService> logger) : IProjectService
    {

        private readonly IProjectRepository _repository = repository;
        private readonly ILogger<ProjectService> _logger = logger;

        public async Task<IEnumerable<ProjectResponseDTO?>> GetAllAsync()
        {

            _logger.LogInformation("Fetching all projects");
            var projects = await _repository.GetAllAsync();

            return projects.Select(p => new ProjectResponseDTO
            {
                ID = p.ID,
                Title = p.Title,
                Description = p.Description,
                TechStack = p.TechStack,
                GitHubUrl = p.GitHubUrl,
                LiveUrl = p.LiveUrl,
                CreatedAt = p.CreatedAt
            });

        }

        public async Task<ProjectResponseDTO?> GetByIDAsync(Guid ID)
        {

            var project = await _repository.GetByIDAsync(ID);

            if (project == null)
            {
                _logger.LogWarning("Project not found for ID: {Id}", ID);
                return null;
            }

            return new ProjectResponseDTO
            {
                ID = project.ID,
                Title = project.Title,
                Description = project.Description,
                TechStack = project.TechStack,
                GitHubUrl = project.GitHubUrl,
                LiveUrl = project.LiveUrl,
                CreatedAt = project.CreatedAt
            };

        }

        public async Task<ProjectResponseDTO?> CreateAsync(ProjectCreateDTO dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                _logger.LogWarning("Project creation failed: Title is empty");
                throw new ArgumentException("Title is required"); 
            }

            if (dto.Title.Length > 100)
            {
                throw new ArgumentException("Title too long"); 
            }

            if (!string.IsNullOrEmpty(dto.GitHubUrl) && !Uri.IsWellFormedUriString(dto.GitHubUrl, UriKind.Absolute))
            {
                _logger.LogWarning("Invalid GitHub URL provided: {Url}", dto.GitHubUrl);
                throw new ArgumentException("Invalid GitHub URL");
            }

            var project = new Project
            {
                ID = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                TechStack = dto.TechStack,
                GitHubUrl = dto.GitHubUrl,
                LiveUrl = dto.LiveUrl,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(project);

            _logger.LogInformation("Creating project with title: {Title}", dto.Title);

            return new ProjectResponseDTO
            {
                ID = project.ID,
                Title = project.Title,
                Description = project.Description,
                TechStack = project.TechStack,
                GitHubUrl = project.GitHubUrl,
                LiveUrl = project.LiveUrl,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task<ProjectUpdateDTO?> UpdateAsync(Guid ID, ProjectUpdateDTO dto)
        {

            var existingProject = await _repository.GetByIDAsync(ID);

            if (existingProject == null)
                return null;

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title is required");

            existingProject.Title = dto.Title;
            existingProject.Description = dto.Description;
            existingProject.TechStack = dto.TechStack;
            existingProject.GitHubUrl = dto.GitHubUrl;
            existingProject.LiveUrl = dto.LiveUrl;

            await _repository.UpdateAsync(ID, existingProject);

            return new ProjectResponseDTO
            {
                ID = existingProject.ID,
                Title = existingProject.Title,
                Description = existingProject.Description,
                TechStack = existingProject.TechStack,
                GitHubUrl = existingProject.GitHubUrl,
                LiveUrl = existingProject.LiveUrl,
                CreatedAt = existingProject.CreatedAt
            };

        }

        public async Task<bool> DeleteAsync(Guid ID)
        {

            var existingProject = await _repository.GetByIDAsync(ID);

            if (existingProject == null)
                return false;

            await _repository.DeleteAsync(ID);

            return true;

        }
    }
}