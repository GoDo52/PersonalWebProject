using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalWeb_Razor.Models;
using PersonalWeb_Razor.Data;

namespace PersonalWeb_Razor.Pages.Categories
{
    // Bind All Properties
    // [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Category.Name != null && _db.Categories.Any(c => c.Name == Category.Name))
            {
                ModelState.AddModelError("name", "category name already exists!");
                return Page();
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();
                TempData["success"] = "Category Created successfully!";
                return RedirectToPage("index");
            }
            else
            {
                return Page();
            }

            //_db.Categories.Add(Category);
            //_db.SaveChanges();
            //TempData["success"] = "Category Created successfully!";
            //return RedirectToPage("Index");
        }

    }
}
