using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Personal.DataAccess.Exceptions;
using Personal.DataAccess.Repository.IRepository;
using Personal.Models;
using Personal.Services;
using Personal.Utility;
using System.Security.Claims;

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

        public IActionResult Index()
        {
            return View();
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

                    User userFromDb = _unitOfWork.User.Get(u => u.Email == userLogin.Email, includeProperties: "Role");

                    // Create user identity and claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userFromDb.UserName),
                        new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()),
                        new Claim(ClaimTypes.Role, userFromDb.Role.Name)
                    };

                    // Make a RememberMe button
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = userLogin.RememberMe // Stay logged in or not
                    };

                    // Sign the user in
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

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
        public IActionResult Logout()
        {
            // Sign out the user (invalidate the authentication cookie)
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
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

        public IActionResult Edit()
        {
            User userFromDb = _unitOfWork.User.Get(u => u.UserName == User.Identity.Name);
            
            AccountEdit accountEdit = new AccountEdit()
            {
                UserName = userFromDb.UserName,
                Email = userFromDb.Email
            };

            return View(accountEdit);
        }

        [HttpPost]
        public IActionResult Edit(AccountEdit accountEdit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User userFromDb = _unitOfWork.User.Get(u => u.UserName == User.Identity.Name);

                    _userService.UpdateUser(accountEdit, userFromDb.Email, userFromDb.UserName, out var user);

                    TempData["success"] = "You have successfully updated your account information!";
                    return RedirectToAction("Index", "Account");
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
            
            return View("Edit", "Account");
        }
    }
}
