using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        List<Chef> AllChefs = _context.Chefs.Include(c => c.AllDishes).OrderBy(c => c.FName).ToList();

        return View(AllChefs);
    }

    public IActionResult ChefForm()
    {
        return View();
    }

    [HttpPost("chefs/create")]
    public IActionResult NewChef(Chef newChef)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newChef);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View("ChefForm");
        }
    }

    public IActionResult DishForm()
    {
        ViewBag.AllChefs = _context.Chefs.OrderBy(c => c.FName).ToList();
        return View();
    }

    [HttpPost("dishes/create")]
    public IActionResult NewDish(Dish newDish)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Dishes");
        }
        else
        {
            ViewBag.AllChefs = _context.Chefs.OrderBy(c => c.FName).ToList();
            return RedirectToAction("DishForm", newDish);
        }
    }

    [HttpGet("dishes")]
    public IActionResult Dishes()
    {
        List<Dish> AllDishes = _context.Dishes.Include(d => d.Chef).ToList();
        return View(AllDishes);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
