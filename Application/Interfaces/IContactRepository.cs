using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact>? GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task CreateAsync(Contact contact);
        Task UpdateAsync(Contact contact);
    }
}
