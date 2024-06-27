using Judaica_2.Data;
using Judaica_2.Models;
using Judaica_2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();
            var viewModel = new VMCreateCategory
            {
                Category = new Category(),
                Categories = categories,
                Parent = new Category() // Or set a specific parent if needed
            };
            return View(viewModel);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(VMCreateCategory VM)
        {
            Category parent = _context.Categories.FirstOrDefault(c => c.ID == VM.ParentID)!;

            if (parent == null)
            {
                ModelState.AddModelError("ParentID", "Parent category not found");
                return View(VM);
            }

            parent
                .AddSubCategory(VM.Category.Name!, VM.Image!);

            if (VM.Item.Name != null && VM.Price > 0)
            {
                parent
                    .AddItem(VM.Item.Name, VM.ImageItem!, VM.Price);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Name,Image")] Category category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                if (!category.Items.Any())
                {
                    category.Items = new();
                }
                _context.Update(category);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));

        }

        // delete category
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
