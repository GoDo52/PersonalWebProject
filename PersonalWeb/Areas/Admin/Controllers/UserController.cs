using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal.DataAccess.Data;
using Personal.DataAccess.Repository.IRepository;
using Personal.Models;
using Personal.Utility;
using Personal.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Personal.DataAccess.Repository;
using Microsoft.IdentityModel.Tokens;
using Personal.DataAccess.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Personal.Services.Interfaces;

namespace PersonalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class UserController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserService _userService;

		public UserController(IUnitOfWork unitOfWork, IUserService userService)
		{
			_unitOfWork = unitOfWork;
			_userService = userService;
		}

		public IActionResult Index()
		{
            List<User> objUserList = _unitOfWork.User.GetAll(includeProperties: "Role").ToList();

            return View(objUserList);
        }

        public IActionResult Upsert(int? id)
        {
            UserVM userVM = new()
            {
                User = new User(),
                RoleList = _unitOfWork.Role.
                GetAll().Select(
                    u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }
                )
            };

            if (id == 0 || id == null)
            {
                // create
                return View(userVM);
            }
            else
            {
                User? userFromDb = _unitOfWork.User.Get(u => u.Id == id);

                if (userFromDb == null)
                {
                    return NotFound();
                }

                userVM.User = userFromDb;

                return View(userVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(UserVM userVM)
        {
			if (ModelState.IsValid)
			{
				UserRegistration userRegistration = new UserRegistration()
				{
					UserName = userVM.User.UserName,
					Email = userVM.User.Email,
					Password = userVM.Password,
				};

                userVM.RoleList = _unitOfWork.Role.
                GetAll().Select(
                    u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }
                );

                // Tries to register or update the user, if any of those fail
                // shows same error massages as the view is basically the same
				try
				{
                    if (userVM.User.Id == 0)
					{
                        _userService.RegisterUser(userRegistration);
                        TempData["success"] = "User Added successfully!";
                        return RedirectToAction("Index", "User");
                    }
					else
					{
                        _unitOfWork.User.Update(userVM.User);
                        _unitOfWork.Save();

                        TempData["success"] = "User Updated successfully!";
                        return RedirectToAction("Index", "User");
                    }
                }
                catch (DuplicateUserNameException ex)
                {
                    ModelState.AddModelError("User.UserName", ex.Message);
                }
                catch (DuplicateUserEmailException ex)
                {
                    ModelState.AddModelError("User.Email", ex.Message);
                }

                return View(userVM);
			}
            else
			{
				userVM.RoleList = _unitOfWork.Role.
				GetAll().Select(
					u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }
				);
                return View(userVM);
			}
        }

        public IActionResult Delete(int? id)
		{
			User? userFromDb = _unitOfWork.User.Get(u => u.Id == id, includeProperties: "Role");
			if (userFromDb == null)
			{
				return NotFound();
			}
			else
			{
				return View(userFromDb);
			}
		}

		[HttpPost]
		public IActionResult Delete(User user)
		{
			if (user == null)
			{
				return NotFound();
			}
			else
			{
				_unitOfWork.User.Remove(user);
				_unitOfWork.Save();
				TempData["success"] = "User Removed successfully";
				return RedirectToAction("Index", "User");
			}
		}
	}
}
