using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalWeb_Razor.Models;
using PersonalWeb_Razor.Data;

namespace PersonalWeb_Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
		private readonly ApplicationDbContext _db;
		[BindProperty]
		public Category Category { get; set; }

		public EditModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public void OnGet(int? id)
		{
			if (id != null && id != 0)
			{
				Category = _db.Categories.Find(id);
			}
		}

		public IActionResult OnPost()
		{
			if (Category.Name != null && _db.Categories.Any(c => c.Name == Category.Name))
			{
				ModelState.AddModelError("name", "Category Name already exists!");
				return Page();
			}
			if (ModelState.IsValid)
			{
				_db.Categories.Update(Category);
				_db.SaveChanges();
                TempData["success"] = "Category Edited successfully!";
                return RedirectToPage("Index");
			}
			else
			{
				return Page();
			}
		}
	}
}
