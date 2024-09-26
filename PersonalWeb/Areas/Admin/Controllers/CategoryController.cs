﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal.DataAccess.Data;
using Personal.DataAccess.Repository.IRepository;
using Personal.Models;

namespace PersonalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            //if (obj.Name != null && _categoryRepository.Categories.Any(c => c.Name == obj.Name))
            //{
            //    ModelState.AddModelError("name", "Category Name already exists!");
            //    return View();
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created successfully!";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
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
            //if (obj.Name != null && _db.Categories.Any(c => c.Name == obj.Name))
            //{
            //    ModelState.AddModelError("name", "Can not be the same name or the existing name!");
            //    return View();
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated successfully!";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
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