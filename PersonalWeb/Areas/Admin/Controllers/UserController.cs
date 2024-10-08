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

namespace PersonalWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public UserController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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
			if (ModelState.IsValid || userVM.Password.IsNullOrEmpty())
			{
				if (userVM.User.Id == 0)
				{
					PasswordHasher hasher = new PasswordHasher(userVM.Password);
					var passwordHash = hasher.MakeHash();
					userVM.User.PasswordHash = passwordHash.Hash;
					userVM.User.PasswordSalt = passwordHash.Salt;

					_unitOfWork.User.Add(userVM.User);
					_unitOfWork.Save();

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
