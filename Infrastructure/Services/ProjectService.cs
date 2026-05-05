using Core.DTOs.Common;
using Core.DTOs.Projects;
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

            return ProjectResponseDTO.ToResponse(project);

        }

        public async Task<ProjectResponseDTO> CreateAsync(ProjectCreateDTO dto)
        {

            ProjectResponseDTO.ValidateProject(dto.Title, dto.GitHubUrl, dto.LiveUrl);

            var project = new Project
            {
                ID = Guid.NewGuid(),
                Title = dto.Title.Trim(),
                Description = dto.Description ?? string.Empty,
                TechStack = dto.TechStack ?? string.Empty,
                GitHubUrl = dto.GitHubUrl ?? string.Empty,
                LiveUrl = dto.LiveUrl ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(project);

            _logger.LogInformation("Created project with ID: {Id}", project.ID);

            return ProjectResponseDTO.ToResponse(project);
        }

        public async Task<ProjectResponseDTO?> UpdateAsync(Guid ID, ProjectUpdateDTO dto)
        {

            var existingProject = await _repository.GetByIDAsync(ID);

            if (existingProject == null)
                return null;

            ProjectResponseDTO.ValidateProject(dto.Title, dto.GitHubUrl, dto.LiveUrl);

            existingProject.Title = dto.Title.Trim();
            existingProject.Description = dto.Description ?? string.Empty;
            existingProject.TechStack = dto.TechStack ?? string.Empty;
            existingProject.GitHubUrl = dto.GitHubUrl ?? string.Empty;
            existingProject.LiveUrl = dto.LiveUrl ?? string.Empty;

            await _repository.UpdateAsync(ID, existingProject);

            return ProjectResponseDTO.ToResponse(existingProject);

        }

        public async Task<bool> DeleteAsync(Guid ID)
        {

            var existingProject = await _repository.GetByIDAsync(ID);

            if (existingProject == null)
                return false;

            await _repository.DeleteAsync(ID);

            return true;

        }

        public async Task<PagedResult<ProjectResponseDTO>> GetAllAsync(ProjectQueryDTO query)
        {

            query.PageNumber = query.PageNumber <= 0 ? 1 : query.PageNumber;
            query.PageSize = query.PageSize <= 0 ? 10 : query.PageSize;
            query.PageSize = Math.Min(query.PageSize, 50);

            var (projects, totalCount) = await _repository.GetPagedAsync(
                query.PageNumber,
                query.PageSize,
                query.Search
            );

            return new PagedResult<ProjectResponseDTO>
            {
                Items = projects.Select(ProjectResponseDTO.ToResponse),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };

        }

    }
}
