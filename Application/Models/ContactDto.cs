using Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ContactDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(100, ErrorMessage = "Имя не может быть длиннее 100 символов")]
        public string Name { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Телефон не может быть длиннее 20 символов")]
        public string MobilePhone { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Должность не может быть длиннее 50 символов")]
        public string JobTitle { get; set; }

        [PastDate]
        [Required]
        public DateTime BirthDate { get; set; }
    }
}