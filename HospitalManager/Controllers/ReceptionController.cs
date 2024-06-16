using HospitalManager.Data;
using HospitalManager.Interfaces;
using HospitalManager.Models;
using HospitalManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace HospitalManager.Controllers
{
    public class ReceptionController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReceptionRepository _ReceptionRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReceptionController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IReceptionRepository ReceptionRepository, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _ReceptionRepository = ReceptionRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Stats()
        {
            var receptions = await _context.Receptions.ToListAsync();
            return View(receptions);
        }
        public async Task<IActionResult> Index(DateTime? Date, string sphere)
        {
            var receptionViewModel = new ReceptionViewModel();
            IQueryable<Reception> receptions_ = _context.Receptions.Include(p => p.Doctor);
            receptionViewModel.receptions = receptions_.ToList();

            if (Date.HasValue)
            {
                receptionViewModel.receptions = (ICollection<Reception>)await _ReceptionRepository.GetReceptionsByDayMonthYear(Date.Value);
                receptionViewModel.SelectedDate = Date.Value;
            }

            if (!String.IsNullOrEmpty(sphere) && sphere != "Все")
            {
                if(Date.HasValue)
                    receptionViewModel.receptions = receptionViewModel.receptions.Where(p => p.Doctor.Specialization == sphere).ToList();
                else
                    receptionViewModel.receptions = receptions_.Where(p => p.Doctor.Specialization == sphere).ToList();
            }
            receptionViewModel.receptions = receptionViewModel.receptions.OrderBy(i => Math.Abs((DateTime.Now - i.Date).TotalMilliseconds)).ToList();
            receptionViewModel.receptions = receptionViewModel.receptions.Where(p => p.Date > DateTime.Now).ToList();

            if(String.IsNullOrEmpty(sphere) && sphere == "Все" || !Date.HasValue)
            {
                List<Reception> receptions_list = new List<Reception>();
                int i = 0;
                foreach(var item in receptionViewModel.receptions)
                {
                    receptions_list.Add(item);
                    i++;
                    if (i == 50)
                        break;
                }
                receptionViewModel.receptions = receptions_list;
            }

            return View(receptionViewModel);
        }

        public async Task<IActionResult> MyReceptions()
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            var userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var receptionVM = new ReceptionViewModel
            {
                receptions = await _context.Receptions.Include(i => i.Doctor).Where(i => i.ClientId == userId).Where(i => i.Date >= DateTime.Now).ToListAsync()
            };
            receptionVM.receptions = receptionVM.receptions.OrderBy(i => Math.Abs((DateTime.Now - i.Date).TotalMilliseconds)).ToList();

            return View(receptionVM);
        }
        public async Task<IActionResult> DoctorReceptions()
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            var userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var receptionVM = new ReceptionViewModel
            {
                receptions = await _context.Receptions.Include(i => i.Client).Where(i => i.DoctorId == userId).Where(i => i.Date >= DateTime.Now).ToListAsync()
            };
            receptionVM.receptions = receptionVM.receptions.OrderBy(i => Math.Abs((DateTime.Now - i.Date).TotalMilliseconds)).ToList();

            return View(receptionVM);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBookReception(int receptionId)
        {
            var reception = await _context.Receptions.FirstOrDefaultAsync(i => i.Id == receptionId);
            if(reception == null)
            {
                return View();
            }

            reception.ClientId = null;
            reception.Client = null;
            _context.Update(reception);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyReceptions", "Reception");
        }

        [HttpPost]
        public async Task<IActionResult> BookReception(int receptionId)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            var clientId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var reception = await _context.Receptions.FirstOrDefaultAsync(i => i.Id == receptionId);
            if(reception == null)
            {
                return View();
            }
            
            reception.ClientId = clientId;
            reception.Client = await _userManager.FindByIdAsync(clientId);
            _context.Update(reception);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Reception");
            
        }

        public async Task<IActionResult> Create()
        {
            var doctorsFromRole = await _userManager.GetUsersInRoleAsync(UserRoles.Doctor);
            var doctors = doctorsFromRole.Select(t => new
            {
                Id = t.Id,
                FullName = $"{t.Name} {t.Surname} {t.MiddleName}"
            }).ToList();

            var selectedList = new SelectList(doctors, "Id", "FullName");
            ViewBag.DoctorList = selectedList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReceptionViewModel createReceptionViewModel)
        {
            if (!ModelState.IsValid)
            {
                var doctorsFromRole = await _userManager.GetUsersInRoleAsync(UserRoles.Doctor);
                var doctors = doctorsFromRole.Select(t => new
                {
                    Id = t.Id,
                    FullName = $"{t.Name} {t.Surname} {t.MiddleName}"
                }).ToList();

                var selectedList = new SelectList(doctors, "Id", "FullName");
                ViewBag.DoctorList = selectedList;
                return View(createReceptionViewModel);
            }  
            if(createReceptionViewModel.Date < DateTime.Now)
            {
                var doctorsFromRole = await _userManager.GetUsersInRoleAsync(UserRoles.Doctor);
                var doctors = doctorsFromRole.Select(t => new
                {
                    Id = t.Id,
                    FullName = $"{t.Name} {t.Surname} {t.MiddleName}"
                }).ToList();

                var selectedList = new SelectList(doctors, "Id", "FullName");
                ViewBag.DoctorList = selectedList;
                TempData["Error"] = "Некорректная дата.";
                return View(createReceptionViewModel);
            }
            var reception = new Reception
            {
                Date = (DateTime)createReceptionViewModel.Date,
                Procedure = createReceptionViewModel.Procedure,
                Price = (double)createReceptionViewModel.Price,
                DoctorId = createReceptionViewModel.DoctorId,
                Doctor = await _userManager.FindByIdAsync(createReceptionViewModel.DoctorId),
            };
            _context.Add(reception);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int id)
        {
            var doctorsFromRole = await _userManager.GetUsersInRoleAsync(UserRoles.Doctor);
            var doctors = doctorsFromRole.Select(t => new
            {
                Id = t.Id,
                FullName = $"{t.Name} {t.Surname} {t.MiddleName}"
            }).ToList();

            var selectedList = new SelectList(doctors, "Id", "FullName");
            ViewBag.DoctorList = selectedList;

            var reception = await _context.Receptions.FirstOrDefaultAsync(i => i.Id == id);
            if (reception == null) return View("Ошибка");
            var editReceptionViewModel = new EditReceptionViewModel
            {
                Date = reception.Date,
                Procedure = reception.Procedure,
                Price = reception.Price,
                DoctorId = reception.DoctorId,
                Doctor = reception.Doctor,
            };
            return View(editReceptionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditReceptionViewModel editReceptionViewModel)
        {
            if(!ModelState.IsValid)
            {
                var doctorsFromRole = await _userManager.GetUsersInRoleAsync(UserRoles.Doctor);
                var doctors = doctorsFromRole.Select(t => new
                {
                    Id = t.Id,
                    FullName = $"{t.Name} {t.Surname} {t.MiddleName}"
                }).ToList();

                var selectedList = new SelectList(doctors, "Id", "FullName");
                ViewBag.DoctorList = selectedList;
                ModelState.AddModelError("", "Не удалось изменить");
                return View("Edit", editReceptionViewModel);
            }
            if (editReceptionViewModel.Date < DateTime.Now)
            {
                var doctorsFromRole = await _userManager.GetUsersInRoleAsync(UserRoles.Doctor);
                var doctors = doctorsFromRole.Select(t => new
                {
                    Id = t.Id,
                    FullName = $"{t.Name} {t.Surname} {t.MiddleName}"
                }).ToList();

                var selectedList = new SelectList(doctors, "Id", "FullName");
                ViewBag.DoctorList = selectedList;
                TempData["Error"] = "Некорректная дата.";
                return View(editReceptionViewModel);
            }
            var userReception = await _context.Receptions.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if(userReception != null)
            {
                var reception = new Reception
                {
                    Id = id,
                    Procedure = editReceptionViewModel.Procedure,
                    Price = (double)editReceptionViewModel.Price,
                    Date = (DateTime)editReceptionViewModel.Date,
                    DoctorId = editReceptionViewModel.DoctorId,
                    Doctor = await _userManager.FindByIdAsync(editReceptionViewModel.DoctorId),
                };
                _context.Update(reception);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Reception");
            }
            else
            {
                return View(editReceptionViewModel);
            }
        }
    
        public async Task<IActionResult> Delete(int id)
        {
            if(ModelState.IsValid)
            {
                var reception = await _context.Receptions.FirstOrDefaultAsync(i => i.Id == id);
                if (_context.Receptions.Any() && reception != null)
                {
                    _context.Remove(reception);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Reception");
        }
    }
}
