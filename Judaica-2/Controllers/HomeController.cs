using Judaica_2.Data;
using Judaica_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Judaica_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Ctx _context; // Add a private field for Ctx

        public HomeController(ILogger<HomeController> logger, Ctx context) // Inject Ctx here
        {
            _logger = logger;
            _context = context; // Assign the injected context to the private field
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
