using DayHospital.Data;
using DayHospital.Data.ViewModel;
using DayHospital.Models;
using DayHospital.Models.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DayHospital.Controllers
{
    public class AdminController(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
    {
        private ApplicationDbContext _context = dbContext;
        private IHttpContextAccessor _contextAccessor = contextAccessor;
        private UserManager<ApplicationUser> _userManager = userManager;
        private SignInManager<ApplicationUser> _signInManager = signInManager;

        private static readonly List<Province> Provinces = new List<Province>
        {
            new Province { Id = 1, Name = "North West" },
            new Province { Id = 2, Name = "Gauteng" },
             new Province { Id = 3, Name = "Eastern Cape" },
        };

        private static readonly List<City> Cities = new List<City>
        {
            new City { Id = 1, Name = "Klerksdorp", ProvinceID = 1 },
            new City { Id = 2, Name = "Maftown", ProvinceID = 1 },
            new City { Id = 3, Name = "Johannesburg", ProvinceID = 2 },
            new City { Id = 4, Name = "Port Elizabeth", ProvinceID = 3 }

        };

        private static readonly List<Suburb> Suburbs = new List<Suburb>
        {
            new Suburb { Id = 1, Name = "Alabama", CityID = 1 },
            new Suburb { Id = 2, Name = "Roodepoort", CityID = 2 },
            new Suburb { Id = 3, Name = "Wilkoppies", CityID = 1 },
            new Suburb { Id = 4, Name = "Summerstrand", CityID = 4 },
        };
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddWard()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddWard(Ward ward)
        {
            if (ModelState.IsValid)
            {
                await _context.Wards.AddAsync(ward);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully added Ward.";
                return RedirectToAction("Index");
            }
            return View(ward);
        }
        public async Task<IActionResult> UpdateBed(int? id)
        {
            ViewBag.Wards = _context.Wards;
            if (id == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            var x = await _context.Beds.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (x == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            return View(x);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBed(Bed bed)
        {
            ViewBag.Wards = _context.Wards;
            if (ModelState.IsValid)
            {
                _context.Beds.Update(bed);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully updated bed";
                return RedirectToAction("Index");
            }
            return View(bed);
        }
        public IActionResult Beds()
        {
            var x = _context.Beds;
            var w = _context.Wards;
            var m = from bed in x
                    join ward in w on bed.WardId equals ward.Id
                    select new ListBeds
                    {
                        BedId = bed.Id,
                        RoomNumber = bed.RoomNumber,
                        Ward = ward.Name,
                        Status = bed.Status,
                    };
            return View(m);
        }
        public IActionResult AddBed()
        {
            ViewBag.Wards = _context.Wards;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBed(Bed bed)
        {
            ViewBag.Wards = _context.Wards;
            if (ModelState.IsValid)
            {
                await _context.Beds.AddAsync(bed);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully linked bed to ward";
                return RedirectToAction("Index");
            }
            return View(bed);
        }
        public async Task<IActionResult> Theaters()
        {
            var x = await _context.Theaters.ToListAsync();
            return View(x);
        }
        public async Task<IActionResult> UpdateTheater(int? id)
        {
            if (id == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            var x = await _context.Theaters.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (x == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            return View(x);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTheater(Theater theater)
        {
            if (ModelState.IsValid)
            {
                _context.Theaters.Update(theater);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully updated Theater";
                return RedirectToAction("Index");
            }
            return View(theater);
        }
        public IActionResult AddTheater()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTheater(Theater theater)
        {
            if (ModelState.IsValid)
            {
                await _context.Theaters.AddAsync(theater);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully added Theater";
                return RedirectToAction(nameof(Index));
            }
            return View(theater);
        }

        public async Task<IActionResult> TreatmentCodes()
        {
            var x = await _context.TreatmentCodes.ToListAsync();
            return View(x);
        }
        public IActionResult AddTreatmentCode()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTreatmentCode(TreatmentCode code)
        {
            if (ModelState.IsValid)
            {
                await _context.TreatmentCodes.AddAsync(code);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully added new treatment code";
                return RedirectToAction("Index");
            }
            return View(code);
        }
        public async Task<IActionResult> UpdateTreatmentCode(int? Id)
        {
            if (Id == null)
            {
                TempData["fail"] = "Sorry, Record Not Found!";
                return RedirectToAction("Index");
            }
            var x = await _context.TreatmentCodes.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (x == null)
            {
                TempData["fail"] = "Sorry, Record Not Found!";
                return RedirectToAction("Index");
            }
            return View(x);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTreatmentCode(TreatmentCode code)
        {
            if (ModelState.IsValid)
            {
                _context.TreatmentCodes.Update(code);
                await _context.SaveChangesAsync();
                TempData["pass"] = $"Successfully updated {code.TreatmentId}";
                return RedirectToAction("Index");
            }
            return View(code);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["result"] = "Successfully Logged out";
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("fname");
            return Redirect("/Identity/Account/Login");
        }
        public async Task<IActionResult> AddDayhospitalRecords(DayHospitalRecords hospitalRecords)
        {
            if (ModelState.IsValid)
            {
                await _context.DayHospitalRecords.AddAsync(hospitalRecords);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully added Day Hospital Records";
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalRecords);
        }
        public async Task<IActionResult> UpdateDayHospitalRecords(int? id)
        {
            if (id == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            var x = await _context.DayHospitalRecords.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (x == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            return View(x);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDayhospitalRecords(DayHospitalRecords hospitalRecords)
        {
            if (ModelState.IsValid)
            {
                _context.DayHospitalRecords.Update(hospitalRecords);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully updated Day Hospital Record";
                return RedirectToAction("Index");
            }
            return View(hospitalRecords);
        }
        public async Task<IActionResult> DayHospitalRecords()
        {
            var x = await _context.DayHospitalRecords.ToListAsync();
            return View(x);
        }
        public IActionResult GetProvince()
        {
            var provinces = Provinces.Select(p => new { id = p.Id, name = p.Name }).ToList();
            return Json(provinces);
        }

        public IActionResult GetCity(int id)
        {
            var cities = Cities.Where(c => c.ProvinceID == id).Select(c => new { id = c.Id, name = c.Name }).ToList();
            return Json(cities);
        }

        public IActionResult GetSuburb(int id)
        {
            var suburbs = Suburbs.Where(s => s.CityID == id).Select(s => new { id = s.Id, name = s.Name }).ToList();
            return Json(suburbs);
        }

        public async Task<IActionResult> AddVital(Vitals vital)
        {
            if (ModelState.IsValid)
            {
                await _context.Vitals.AddAsync(vital);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully added Vital";
                return RedirectToAction(nameof(Index));
            }
            return View(vital);
        }

        public async Task<IActionResult> UpdateVital(int? id)
        {
            if (id == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            var x = await _context.Vitals.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (x == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            return View(x);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateVital(Vitals vitals)
        {
            if (ModelState.IsValid)
            {
                _context.Vitals.Update(vitals);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully updated Vital";
                return RedirectToAction("Index");
            }
            return View(vitals);
        }
        public async Task<IActionResult> Vitals()
        {
            var x = await _context.Vitals.ToListAsync();
            return View(x);
        }


        public async Task<IActionResult> AddChronicCondition(ChronicCondition chronicCondition)
        {
            if (ModelState.IsValid)
            {
                await _context.ChronicConditions.AddAsync(chronicCondition);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully added Chronic Condition";
                return RedirectToAction(nameof(Index));
            }
            return View(chronicCondition);
        }

        public async Task<IActionResult> UpdateChronicCondition(int? id)
        {
            if (id == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            var x = await _context.ChronicConditions.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (x == null)
            {
                TempData["fail"] = "Record not found.";
                return RedirectToAction("Index");
            }
            return View(x);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateChronicCondition(ChronicCondition chronicCondition)
        {
            if (ModelState.IsValid)
            {
                _context.ChronicConditions.Update(chronicCondition);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Successfully updated Chronic Condition";
                return RedirectToAction("Index");
            }
            return View(chronicCondition);
        }
        public async Task<IActionResult> ChronicConditions()
        {
            var x = await _context.ChronicConditions.ToListAsync();
            return View(x);
        }
    }
}
