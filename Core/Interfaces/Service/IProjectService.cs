using Core.DTOs.Common;
using Core.DTOs.Projects;

namespace Core.Interfaces.Service
{
    public interface IProjectService
    {

        Task<PagedResult<ProjectResponseDTO>> GetAllAsync(ProjectQueryDTO query);
        Task<ProjectResponseDTO?> GetByIDAsync(Guid ID);
        Task<ProjectResponseDTO?> CreateAsync(ProjectCreateDTO dto);
        Task<ProjectResponseDTO?> UpdateAsync(Guid ID, ProjectUpdateDTO dto);
        Task<bool> DeleteAsync(Guid ID);

    }
}
