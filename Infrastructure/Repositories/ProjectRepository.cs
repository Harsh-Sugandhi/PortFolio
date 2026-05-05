using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository(AppDbContext dbContext) : IProjectRepository
    {

        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project?> GetByIDAsync(Guid ID)
        {
            return await _dbContext.Projects.FindAsync(ID);
        }

        public async Task<Project> CreateAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            return project;
        }

        public async Task<Project?> UpdateAsync(Guid ID, Project project)
        {
            var existing = await _dbContext.Projects.FindAsync(ID);

            if (existing == null)
                return null;

            existing.Title = project.Title;
            existing.Description = project.Description;
            existing.TechStack = project.TechStack;
            existing.GitHubUrl = project.GitHubUrl;
            existing.LiveUrl = project.LiveUrl;

            await _dbContext.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(Guid ID)
        {
            var existing = await _dbContext.Projects.FindAsync(ID);

            if (existing == null)
                return false;

            _dbContext.Projects.Remove(existing);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<(IEnumerable<Project> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string? search)
        {

            var query = _dbContext.Projects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Title.Contains(search) ||
                    p.TechStack.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);

        }

    }

}
