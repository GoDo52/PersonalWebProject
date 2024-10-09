using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Personal.DataAccess.Repository.IRepository;
using Personal.Models;
using Personal.Models.ViewModels;
using Personal.Utility;
using System.Security.Claims;

namespace PersonalWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class SpendingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpendingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        // TODO: Add analytics
        public IActionResult Analytics()
        {
            return View();
        }

        public IActionResult AnalyticsTable()
        {
            List<Spending> objSpendingList = _unitOfWork.Spending.GetAll(includeProperties: "Category,User").ToList();

            return View(objSpendingList);
        }

        public IActionResult Upsert(int? id)
        {
            SpendingVM spendingVM = new()
            {
                Spending = new Spending(),
                CategoryList = _unitOfWork.Category.
                GetAll().Select(
                    u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }
                ),
                UserList = _unitOfWork.User.
                GetAll().Select(
                    u => new SelectListItem { Text = u.UserName, Value = u.Id.ToString() }
                )
            };

            if (id == null || id == 0)
            {
                // create
                return View(spendingVM);
            }
            else
            {
                // update
                Spending? spendingFromDb = _unitOfWork.Spending.Get(u => u.Id == id);

                if (spendingFromDb == null)
                {
                    return NotFound();
                }

                spendingVM.Spending = spendingFromDb;

                return View(spendingVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(SpendingVM spendingVM)
        {
            if (ModelState.IsValid)
            {
                if (spendingVM.Spending.Id == 0)
                {
                    if (spendingVM.Spending.UserId == 0)
                    {
                        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                        spendingVM.Spending.UserId = Convert.ToInt32(userIdString);
                    }

                    spendingVM.Spending.DateTime = DateTime.Now;
                    _unitOfWork.Spending.Add(spendingVM.Spending);
                    _unitOfWork.Save();
                    TempData["success"] = "Spending Added successfully!";
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
                else
                {
                    //spendingVM.Spending.UserId = 1;
                    _unitOfWork.Spending.Update(spendingVM.Spending);
                    _unitOfWork.Save();
                    TempData["success"] = "Spending Updated successfully!";
                    return RedirectToAction("AnalyticsTable", "Spending");
                }
            }
            else
            {
                spendingVM.CategoryList = _unitOfWork.Category.
                GetAll().Select(
                    u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }
                );
                spendingVM.UserList = _unitOfWork.User.
                GetAll().Select(
                    u => new SelectListItem { Text = u.UserName, Value = u.Id.ToString() }
                );
                return View(spendingVM);
            }
        }

        public IActionResult Delete(int? id)
        {
            Spending? spendingFromDb = _unitOfWork.Spending.Get(u => u.Id == id);
            if (spendingFromDb == null)
            {
                return NotFound();
            }
            else
            {
                return View(spendingFromDb);
            }
        }

        [HttpPost]
        public IActionResult Delete(Spending spending)
        {
            if (spending == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Spending.Remove(spending);
                _unitOfWork.Save();
                TempData["success"] = "Spending Removed successfully";
                return RedirectToAction("AnalyticsTable", "Spending");
            }
        }
    }
}