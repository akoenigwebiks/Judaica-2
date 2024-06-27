using Judaica_2.Data;
using Judaica_2.Models;
using Judaica_2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Judaica_2.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Ctx _context; // Add a private field for Ctx

        public ManagerController(ILogger<HomeController> logger, Ctx context) // Inject Ctx here
        {
            _logger = logger;
            _context = context; // Assign the injected context to the private field
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(VMCreateCategory VM)
        {
            Category parent = _context.Categories.FirstOrDefault(c => c.ID == VM.ParentID)!;

            if (parent is not null)
            {
                parent
                    .AddSubCategory(VM.Category.Name!, VM.Image!);
                parent
                    .AddItem(VM.Item.Name, VM.ImageItem!, VM.Price);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
    }
}
