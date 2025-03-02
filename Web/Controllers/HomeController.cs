using Data.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
public class HomeController : Controller
{
    private readonly WebDbContext _context;

    public HomeController(WebDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories
                                 .Include(c => c.Products)
                                 .ThenInclude(p => p.ProductImages) 
                                 .ToList();
        return View(categories);  
    }
}
