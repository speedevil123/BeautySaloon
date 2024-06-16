using HospitalManager.Data;
using HospitalManager.Models;
using HospitalManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymChanger.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public RoleController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string sphere, string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                if (!await _userManager.IsInRoleAsync(user, UserRoles.Doctor))
                {
                    if (user.DoctorsReceptions != null)
                    {
                        user.DoctorsReceptions.Clear();
                        var receptionsId = _context.Receptions.Where(i => i.DoctorId == userId);
                        _context.Receptions.RemoveRange(receptionsId);
                    }
                }
                else if(await _userManager.IsInRoleAsync(user, UserRoles.Doctor) && sphere != null)
                {
                    user.Specialization = sphere;
                }
                else
                {
                    TempData["Error"] = "Выберите роль.";
                    ChangeRoleViewModel model = new ChangeRoleViewModel
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        UserRoles = userRoles,
                        AllRoles = allRoles
                    };
                    return View(model);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
