using Core.Entities;


namespace Core.Interfaces
{

    public interface IProjectRepository
    {

        Task<IEnumerable<Project>> FetchProjectListAsync();
        Task<Project?> FetchProjectByIDAsync(Guid ID);
        Task AddProjectAsync(Project project);

    }

}
