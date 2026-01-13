using Data.Interfaces;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using TestSIAREM.Models;

namespace TestSIAREM.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactRepository _repository;

        public ContactsController(IContactRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _repository.GetAllAsync();
            return Ok(contacts); 
        }

        [HttpPost]
        public async Task<IActionResult> SaveContact([FromBody] ContactDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (contactDto.Id == 0)
                {
                    var contact = new Contact
                    {
                        Name = contactDto.Name,
                        MobilePhone = contactDto.MobilePhone,
                        JobTitle = contactDto.JobTitle,
                        BirthDate = contactDto.BirthDate
                    };

                    await _repository.CreateAsync(contact);
                }
                else
                {
                    var existingContact = await _repository.GetByIdAsync(contactDto.Id);

                    if (existingContact == null)
                    {
                        return NotFound($"Контакт с ID {contactDto.Id} не найден.");
                    }

                    existingContact.Name = contactDto.Name;
                    existingContact.MobilePhone = contactDto.MobilePhone;
                    existingContact.JobTitle = contactDto.JobTitle;
                    existingContact.BirthDate = contactDto.BirthDate;

                    await _repository.UpdateAsync(existingContact);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ошибка: " + ex.Message);
            }
        }

        [HttpPost] 
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}