#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required(ErrorMessage = "You must name your dish.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Is this your dish or someone else's.")]

    public int Tastiness { get; set; }
    [Required(ErrorMessage = "We need to know how much effort this will take to burn off.")]
    [Range(0, 999999999, ErrorMessage = "This must be a positive number.")]

    public int Calories { get; set; }
    public int ChefId { get; set; }

    public Chef? Chef { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}