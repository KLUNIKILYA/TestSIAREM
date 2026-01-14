using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAllContactsAsync();
        Task SaveContactAsync(ContactDto contactDto);
        Task DeleteContactAsync(int id);
    }
}