using Core.DTOs;

namespace Core.Interfaces.Service
{
    public interface IProjectService
    {

        Task<IEnumerable<ProjectResponseDTO?>> GetAllAsync();
        Task<ProjectResponseDTO?> GetByIDAsync(Guid ID);
        Task<ProjectResponseDTO?> CreateAsync(ProjectCreateDTO dto);
        Task<ProjectUpdateDTO?> UpdateAsync(Guid ID, ProjectUpdateDTO dto);
        Task<bool> DeleteAsync(Guid ID);

    }
}
