using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.AsNoTracking().ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Contacts.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}
