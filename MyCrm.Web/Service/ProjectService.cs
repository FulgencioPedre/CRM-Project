using Microsoft.EntityFrameworkCore;
using MyCrm.Shared;
using MyCrm.Web.Data;
using System.Runtime.Serialization;
using static MyCrm.Shared.Project;

namespace MyCrm.Web.Service
{
    public class ProjectService
    {
        private readonly AppDbContext context;

        public ProjectService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Project pr)
        {
            context.Projects.Add(pr);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var project = await context.Projects
                .FirstOrDefaultAsync(pr => pr.Id == id);

            if (project != null)
            {
                project.IsActive = false;
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Project pr)
        {
            var projectDb = await context.Projects.FindAsync(pr.Id);

            if (projectDb != null)
            {
                if (!string.IsNullOrEmpty(pr.Name)) projectDb.Name = pr.Name;
                if (!string.IsNullOrEmpty(pr.Description)) projectDb.Description = pr.Description;
                if (pr.StartingDate != default) projectDb.StartingDate = pr.StartingDate;

                if (projectDb.Status == ProjectStatus.Finished)
                {
                    projectDb.EndingDate = default;
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateStatus(int id, ProjectStatus newStatus)
        {
            var projectDb = await context.Projects.FindAsync(id);

            if (projectDb != null)
            {
                projectDb.Status = newStatus;
                await context.SaveChangesAsync();

                if(projectDb.Status == ProjectStatus.Finished)
                {
                    projectDb.EndingDate = default;
                }
            }
        }

        public async Task<List<Project>> GetAll()
        {
            return await context.Projects
                .Include(pr => pr.Company)
                .Where(pr => pr.IsActive)
                .OrderBy(pr => pr.Name)
                .ToListAsync();
        }

        public async Task<List<Project>> GetFiltered(string term)
        {
            return await context.Projects
                .Where(pr => pr.IsActive && (pr.Name.Contains(term)))
                .ToListAsync();
        }

        public async Task<Project?> GetById(int id)
        {
            return await context.Projects
                .Include(pr => pr.Company)
                .FirstOrDefaultAsync(pr => pr.Id == id);
        }

        public async Task<bool> Exists(string name)
        {
            return await context.Projects.AnyAsync(pr => pr.Name == name);
        }
    }
}
