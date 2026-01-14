using System.ComponentModel.DataAnnotations;

namespace Application.Attributes
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (date > DateTime.Now)
            {
                return new ValidationResult("Дата рождения не может быть в будущем");
            }

            if (date.Year < 1900)
            {
                return new ValidationResult("Год рождения должен быть не ранее 1900");
            }

            return ValidationResult.Success;
        }
    }
}