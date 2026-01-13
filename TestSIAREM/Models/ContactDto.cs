using System.ComponentModel.DataAnnotations;

namespace TestSIAREM.Models
{
    public class ContactDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Должность обязательна")]
        public string JobTitle { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}