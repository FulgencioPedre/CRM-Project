using Microsoft.EntityFrameworkCore;
using MyCrm.Shared;
using MyCrm.Web.Data;

namespace MyCrm.Web.Service
{
    public class ContactService
    {
        private readonly AppDbContext context;

        public ContactService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Contact c)
        {
            context.Contacts.Add(c);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var contact = await context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact != null) {
                contact.IsActive = false;
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Contact c)
        {
            var contactDb = await context.Contacts.FindAsync(c.Id);

            if (contactDb != null)
            {
                if (!string.IsNullOrEmpty(c.Name)) contactDb.Name = c.Name;
                if (!string.IsNullOrEmpty(c.Surname)) contactDb.Surname = c.Surname;
                if (!string.IsNullOrEmpty(c.Email)) contactDb.Email = c.Email;
                if (!string.IsNullOrEmpty(c.Phone)) contactDb.Phone = c.Phone;
                if (c.CompanyId != 0) contactDb.CompanyId = c.CompanyId;
            }

            await context.SaveChangesAsync();
        }

        public async Task<List<Contact>> GetAll()
        {
            return await context.Contacts
                .Include(c => c.Company)
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<List<Contact>> GetFiltered(string term)
        {
            return await context.Contacts
                .Where(c => c.IsActive && (c.Name.Contains(term)))
                .ToListAsync();
        }

        public async Task<Contact?> GetById(int id)
        {
            return await context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Contact>> GetByCompany(int companyId)
        {
            return await context.Contacts
                .Where(c => c.IsActive && (c.CompanyId == companyId))
                .ToListAsync();
        }

        public async Task<bool> Exists(string email)
        {
            return await context.Contacts.AnyAsync(c => c.Email == email);
        }
    }
}
