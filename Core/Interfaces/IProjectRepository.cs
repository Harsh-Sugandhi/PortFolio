using Core.DTOs;
using Core.Entities;


namespace Core.Interfaces
{

    public interface IProjectRepository
    {

        Task<IEnumerable<Project?>> GetAllAsync();
        Task<Project?> GetByIDAsync(Guid ID);
        Task<Project?> CreateAsync(Project project);
        Task<Project?> UpdateAsync(Guid ID, Project project);
        Task<bool> DeleteAsync(Guid ID);
    }

}
