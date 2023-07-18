#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;
public class Chef
{
    [Key]
    public int ChefId { get; set; }
    [Required(ErrorMessage = "What's your chef's first name?")]
    public string FName { get; set; }
    [Required(ErrorMessage = "What's your chef's last name?")]
    public string LName { get; set; }

    [DataType(DataType.Date)]
    [Birthday]
    public DateTime DOB { get; set; }

    public List<Dish> AllDishes { get; set; } = new List<Dish>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class BirthdayAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            if (date > DateTime.Now)
            {
                return new ValidationResult("Date cannot be in the future.");
            }

            var minDate = DateTime.Now.AddYears(-18);
            if (date > minDate)
            {
                return new ValidationResult("You must be at least 18 years old to contribute.");
            }
        }

        return ValidationResult.Success;
    }
}