using Microsoft.AspNetCore.Mvc;
using Personal.DataAccess.Repository.IRepository;
using Personal.Models;
using Personal.Utility;

namespace PersonalWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                PasswordHasher passwordHasher = new PasswordHasher(userLogin.Password);

                // Retrieve user by email and get salt and hash
                User user = _unitOfWork.User.Get(u => u.Email == userLogin.Email);
                string saltFromDb = user.PasswordSalt;
                string hashFromDb = user.PasswordHash;

                bool checkHash = passwordHasher.VerifyPassword(hashFromDb, saltFromDb);

                if (user != null && checkHash)
                {
                    // TODO: Set up the session to track user login

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                    return View(userLogin);
                }
            }
            else
            {
                return View(userLogin);
            }
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
                PasswordHasher hasher = new PasswordHasher(userRegistration.Password);
                var passwordHash = hasher.MakeHash();

                User newUser = new User
                {
                    UserName = userRegistration.UserName,
                    Email = userRegistration.Email,
                    PasswordHash = passwordHash.Hash,
                    PasswordSalt = passwordHash.Salt,
                    RoleId = SD.DefaultRoleId
                };

                _unitOfWork.User.Add(newUser);
                _unitOfWork.Save();

                TempData["success"] = "You have successfully registered!";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(userRegistration);
            }
        }
    }
}
