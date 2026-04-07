using MyCrm.Web.Data;
using Microsoft.EntityFrameworkCore;
using MyCrm.Shared;

namespace MyCrm.Web.Service
{
    public class CompanyService
    {
        private readonly AppDbContext context;

        public CompanyService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Company c)
        {
            context.Companies.Add(c);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var company = await context.Companies
                .Include(c => c.Contacts)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (company != null)
            {
                company.IsActive = false;

                foreach(var contact in company.Contacts)
                {
                    contact.IsActive = false;
                }
            }

            await context.SaveChangesAsync();

        }

        public async Task Update(Company c)
        {
            var companyDb = await context.Companies.FindAsync(c.Id);

            if (companyDb != null) { 
                if(!string.IsNullOrEmpty(c.Name)) companyDb.Name = c.Name;
                if(!string.IsNullOrEmpty(c.CIF)) companyDb.CIF = c.CIF;
                if (!string.IsNullOrEmpty(c.Address)) companyDb.Address = c.Address;
                if(!string.IsNullOrEmpty(c.Country)) companyDb.Country = c.Country;
            }

            await context.SaveChangesAsync();
        }

        public async Task<List<Company>> GetAll()
        {
            return await context.Companies
                .Include(c => c.Contacts)
                .Include(c => c.Projects)
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<List<Company>> GetFiltered(string term)
        {
            return await context.Companies
                .Where(c=> c.IsActive && (c.Name.Contains(term) || c.CIF.Contains(term)))
                .ToListAsync();
        }

        public async Task<Company?> GetById(int id)
        {
            return await context.Companies
                .Include(c => c.Contacts)
                .Include(c => c.Projects)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Exists(string cif)
        {
            return await context.Companies.AnyAsync(c => c.CIF == cif);
        }
    }
}
