using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина имени от 2 до 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        [Phone(ErrorMessage = "Некорректный формат телефона")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Должность обязательна")]
        public string JobTitle { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
