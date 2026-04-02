using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{

    public class ProjectRepository(AppDbContext dbContext) : IProjectRepository
    {

        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<Project>> FetchProjectListAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project?> FetchProjectByIDAsync(Guid ID)
        {
            return await _dbContext.Projects.FindAsync(ID);
        }

        public async Task AddProjectAsync(Project project)
        {

            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

        }

    }

}
