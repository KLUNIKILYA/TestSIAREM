using Application.Interfaces;
using Application.Models;
using Data.Models;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            var contacts = await _repository.GetAllAsync();

            return contacts.Select(c => new ContactDto
            {
                Id = c.Id,
                Name = c.Name,
                MobilePhone = c.MobilePhone,
                JobTitle = c.JobTitle,
                BirthDate = c.BirthDate
            });
        }

        public async Task SaveContactAsync(ContactDto contactDto)
        {
            if (contactDto.Id == 0)
            {
                var contact = new Contact
                {
                    Name = contactDto.Name,
                    MobilePhone = contactDto.MobilePhone,
                    JobTitle = contactDto.JobTitle,
                    BirthDate = contactDto.BirthDate,
                };

                await _repository.CreateAsync(contact);
            }
            else
            {
                var existingContact = await _repository.GetByIdAsync(contactDto.Id);

                if (existingContact == null)
                {
                    throw new KeyNotFoundException($"Контакт с ID {contactDto.Id} не найден.");
                }

                existingContact.Name = contactDto.Name;
                existingContact.MobilePhone = contactDto.MobilePhone;
                existingContact.JobTitle = contactDto.JobTitle;
                existingContact.BirthDate = contactDto.BirthDate;

                await _repository.UpdateAsync(existingContact);
            }
        }

        public async Task DeleteContactAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}