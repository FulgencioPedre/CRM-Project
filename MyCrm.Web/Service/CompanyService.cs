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

        public async Task AddCompany(Company c)
        {
            context.Companies.Add(c);
            await context.SaveChangesAsync();
        }
    }
}
