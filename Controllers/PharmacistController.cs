using DayHospital.Data;
using DayHospital.Models;
using DayHospital.Models.Admin;
using DayHospital.Models.Pharmacist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DayHospital.Controllers
{
    public class PharmacistController : Controller
    {
        private ApplicationDbContext _context;

        public PharmacistController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();

        }
        //Viewing all med inventory
        public IActionResult ViewMedication()
        {
            return View();
        } 
        //Add Medication
        public IActionResult AddMedication()
        {
            ViewBag.ActiveIngredients = _context.ActiveIngredients.ToList(); 
            ViewBag.Dosages=_context.Dosages.ToList(); 
            ViewBag.MedicationSchedules= _context.MedicineSchedules.ToList(); 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> AddMedication(Medicine med,List<int>ActiveIngred,List<string>Strengths)
        {
            if (ModelState.IsValid)
            {
                _context.Medicines.Add(med);
                await _context.SaveChangesAsync();

                var medicinActiveIngr=ActiveIngred.Select((id,Index) => new MedicineActiveIngredient
                { 
                    MedicineId=med.Id,
                    ActiveIngredientId=id,
                    Strength = Strengths.Count()
                }).ToList();

                _context.MedicineActiveIngredients.AddRange(medicinActiveIngr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AddMedication));
            }
            ViewBag.MedicationSchedules = _context.MedicineSchedules.ToList();
            ViewBag.Dosages = _context.Dosages.ToList();
            ViewBag.ActiveIngredients = _context.ActiveIngredients.ToList();
            return View(med);
        }

        public IActionResult EditMedication()
        {
            return View();
        }
        public IActionResult ViewPrescription()
        {
            return View();
        }
        public IActionResult PrescriptionSlip()
        {
            return View();
        }
        public IActionResult StockLevels()
        {
            return View();
        }
        public IActionResult OrderStock()
        {
            return View();
        }
        public IActionResult PharmaDashboard()
        {
            return View();
        }
        public IActionResult PatientMedHistory()
        {
            return View();
        }
        public IActionResult PatientHistory()
        {
            return View();
        }
        public IActionResult ReOrderStockView()
        {
            return View();
        }
        public IActionResult ActivePres()
        {
            return View();
        }
        public IActionResult ReceivedStock()
        {
            return View();
        }
    }
}
