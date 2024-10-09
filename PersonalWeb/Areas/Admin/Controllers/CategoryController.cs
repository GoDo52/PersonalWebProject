using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal.DataAccess.Data;
using Personal.DataAccess.Exceptions;
using Personal.DataAccess.Repository.IRepository;
using Personal.Models;
using Personal.Utility;

namespace PersonalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Category.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Category Created successfully!";
                    return RedirectToAction("Index", "Category");
                }
                catch (DuplicateCategoryException ex)
                {
                    ModelState.AddModelError("Name", ex.Message);
                    return View(obj);
                }
            }
            else
            {
                return View(obj);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Category.Update(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Category Updated successfully!";
                    return RedirectToAction("Index", "Category");
                }
                catch (DuplicateCategoryException ex)
                {
                    ModelState.AddModelError("Name", ex.Message);
                    return View(obj);
                }
            }
            else
            {
                return View(obj);
            }
        }

        public IActionResult Delete(int? id)
        {
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            else
            {
                return View(categoryFromDb);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(Category obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Deleted successfully!";
                return RedirectToAction("Index", "Category");
            }
        }
    }
}