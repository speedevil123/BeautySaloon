using HospitalManager.Data;
using HospitalManager.Models;
using HospitalManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace HospitalManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = _httpContextAccessor.HttpContext.User;

            var userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.FindByIdAsync(userId).Result;

            var userRoles = await _userManager.GetRolesAsync(user);
            var Role = userRoles.FirstOrDefault();
            if(Role != null)
            {
                var profileVM = new ProfileViewModel()
                {
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    MiddleName = user.MiddleName,
                    BirthDay = user.BirthDay.ToString("d", CultureInfo.CreateSpecificCulture("de-De")),
                    Role = Role,
                };
                return View(profileVM);
            }
            return View();

        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Reception");
                    }
                }
            }
            TempData["Error"] = "Неверные данные. Попробуйте снова!";
            return View(loginViewModel);
        }


        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Этот email адрес уже используется";
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
                Surname = registerViewModel.Surname,
                Name = registerViewModel.Name,
                MiddleName = registerViewModel.MiddleName,
                BirthDay = registerViewModel.BirthDay
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            await _signInManager.SignInAsync(newUser, isPersistent: false);
            return RedirectToAction("Index", "Reception");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Reception");
        }
    }
}
