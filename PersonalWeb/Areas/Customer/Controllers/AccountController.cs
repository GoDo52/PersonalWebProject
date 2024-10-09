using Microsoft.AspNetCore.Mvc;
using Personal.DataAccess.Exceptions;
using Personal.DataAccess.Repository.IRepository;
using Personal.Models;
using Personal.Services;
using Personal.Utility;

namespace PersonalWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                // TODO: implement exceptions???
                if (_userService.VerifyUserOnLogin(userLogin.Email, userLogin.Password, out var user))
                {
					// TODO: Set up the session to track user login
					TempData["success"] = "Login successful!";
					return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", "Invalid email or password");
                    ModelState.AddModelError("Password", "Invalid email or password");
                }
            }
            
            return View(userLogin);
            
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.RegisterUser(userRegistration);
                    TempData["success"] = "You have successfully registered!";
                    return RedirectToAction("Login", "Account");
                }
                catch (DuplicateUserNameException ex)
                {
					ModelState.AddModelError("UserName", ex.Message);
				}
                catch (DuplicateUserEmailException ex)
                {
					ModelState.AddModelError("Email", ex.Message);
				}
            }

            return View(userRegistration);

        }
    }
}
