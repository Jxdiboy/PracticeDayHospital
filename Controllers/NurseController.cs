using DayHospital.Data;
using DayHospital.Data.ViewModel;
using DayHospital.Models;
using DayHospital.Models.Admin;
using DayHospital.Models.Nurse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security;
using static DayHospital.Data.ViewModel.PrescriptionDetailsVM;



namespace DayHospital.Controllers
{

    public class NurseController(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider) : Controller
    {
        private ApplicationDbContext _context = dbContext;
        private IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly ICompositeViewEngine _viewEngine = viewEngine;
        private readonly ITempDataProvider _tempDataProvider = tempDataProvider;

        // Example data
        private static readonly List<Province> Provinces = new List<Province>
        {
            new Province { Id = 1, Name = "North West" },
            new Province { Id = 2, Name = "Gauteng" }
        };

        private static readonly List<City> Cities = new List<City>
        {
            new City { Id = 1, Name = "Klerksdorp", ProvinceID = 1 },
            new City { Id = 2, Name = "Maftown", ProvinceID = 1 },
            new City { Id = 3, Name = "Johannesburg", ProvinceID = 2 }
        };

        private static readonly List<Suburb> Suburbs = new List<Suburb>
        {
            new Suburb { Id = 1, Name = "Alabama", CityID = 1 },
            new Suburb { Id = 2, Name = "Roodepoort", CityID = 2 },
            new Suburb { Id = 3, Name = "Wilkoppies", CityID = 3 }
        };



        static PatientUser user = new()
        {
            Name = "Jade Appels",
            Email = "jfappels@gmail.com",
            Gender = "Male",
            PhoneNumber = "0829607186",
            IDNumber = "0109105229081",
        };


        static List<BookingAppointmentVM1> d = [
                      new BookingAppointmentVM1
                        {
                            BookingId = 1,
                            Date = "15 Feb 2024",
                            PatientIDNumber = "1234567891234",
                            Time = "14:00",
                            Theater = "Light house",
                        },
                        new BookingAppointmentVM1
                        {
                            BookingId = 2,
                            Date = "1 Feb 2024",
                            PatientIDNumber = "1234567891234",
                            Time = "10:00",
                            Theater = "Light house",
                        },
                        new BookingAppointmentVM1
                        {
                            BookingId = 3,
                            Date = "10 Feb 2024",
                            PatientIDNumber = "1234567891234",
                            Time = "17:00",
                            Theater = "Light house",
                        },

                    ];
        static List<PrescriptionVM1> ps = [
                      new PrescriptionVM1
                        {
                            PrescriptionId = 1,
                            Date = "12/02/2024",
                            PatientIDNumber = "1234567891234",
                            Priority = "High",
                            Status = "Dispensed",
                            PatientName = "Simon Peter"
                        },
                        new PrescriptionVM1
                        {
                            PrescriptionId = 2,
                            Date = "10/04/2024",
                            PatientIDNumber = "1234567891234",
                            Priority = "Medium",
                            Status = "Assss",
                            PatientName = "Mary "
                        },
                        new PrescriptionVM1
                        {
                            PrescriptionId = 3,
                            Date = "10/05/2024",
                            PatientIDNumber = "1234567891234",
                            Priority = "Low",
                            Status = "Rejected",
                            PatientName = "john stones"
                        },
                        new PrescriptionVM1
                        {
                            PrescriptionId = 3,
                            Date = "10/06/2024",
                            PatientIDNumber = "1234567891234",
                            Priority = "Low",
                            Status = "Rejected",
                            PatientName = "john stones"
                        },

                    ];

        private static List<PatientUser> Patients = new List<PatientUser>
        {
            new PatientUser
            {
            Name = "Jade Appels",
            Email = "jfappels@gmail.com",
            Gender = "Male",
            PhoneNumber = "0829607186",
            IDNumber = "0109105229081",
            AddressLine1 = "9 Benting Street, Alabama, Klerksdorp, 2577",

            }

        };
        // Retrieve received prescriptions and associated medications from the database or any other data source
        static List<ReceivedPrescriptionVM> receivedPrescriptions = new List<ReceivedPrescriptionVM>
        {
            new ReceivedPrescriptionVM
            {
                PrescriptionId = 1,
                PatientName = "Jade Appels",
                Ward = "1",
                Bed ="20",
                Medications = new List<MedicationVM>
                {
                    new MedicationVM { MedicationName = "Panado", Quantity = 1, Frequency = "twice a day" },
                    new MedicationVM { MedicationName = "Ibuprofen", Quantity = 2, Frequency = "three times a day" }
                }
            },
            new ReceivedPrescriptionVM
            {
                PrescriptionId = 2,
                PatientName = "Jane Smith",
                Ward = "2",
                Bed = "1",
                Medications = new List<MedicationVM>
                {
                    new MedicationVM { MedicationName = "Amoxicillin",  Frequency = "four times a day",Quantity = 2 },
                    new MedicationVM { MedicationName = "Loratadine",  Frequency = "once a day", Quantity = 10 }
                }
            }
            // Add more received prescriptions as needed
        };



        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var bookings = await _context.SurgeryBookings
                .Where(b => b.Date.Date == today)
                .Join(
                    _context.PatientUsers,
                    booking => booking.PatientIDNumber,
                    patient => patient.IDNumber,
                    (booking, patient) => new
                    {
                        booking.Id,
                        booking.PatientIDNumber,
                        PatientName = patient.Name,
                        PatientSurname = patient.Surname,
                        booking.TheaterId,
                        booking.SurgeonId,
                        booking.Date,
                        booking.Session
                    })
                .ToListAsync();

            return View(bookings);
        }
       
        public async Task<IActionResult> Admit(int id)
        {
            var booking = await _context.SurgeryBookings
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();

            if (booking == null)
            {
                TempData["error"] = "Booking not found";
                return RedirectToAction("Index");
            }

            var patient = await _context.PatientUsers
                .Where(p => p.IDNumber == booking.PatientIDNumber)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                TempData["error"] = "Patient not found";
                return RedirectToAction("Index");
            }

            var treatmentCode = await _context.SurgeryBookingTreatmentCodes
                .Where(tc => tc.BookingId == id)
                .Select(tc => tc.TreatmentCode)
                .FirstOrDefaultAsync();

            if (treatmentCode == null)
            {
                TempData["error"] = "Treatment code not found";
                return RedirectToAction("Index");
            }

            var admittedPatient = new AdmittedPatient
            {
                NurseId = "1234", // Assuming the nurse is logged in
                PatientID = patient.Id.ToString(),
                BookingID = booking.Id,
                TreatmentCode = treatmentCode,
                DateTime = DateTime.Now
            };

            _context.AdmittedPatients.Add(admittedPatient);
            await _context.SaveChangesAsync();

            return RedirectToAction("AdmitPatient", new { id = admittedPatient.Id });
        }



        public IActionResult AddPatient()
        {
            return View();
        }
        // Endpoint to get the list of provinces
        [HttpGet]
        public IActionResult GetProvince()
        {
            return Json(Provinces);
        }

        // Endpoint to get the list of cities by province id
        [HttpGet]
        public IActionResult GetCity(int id)
        {
            var cities = Cities.Where(city => city.ProvinceID == id).ToList();
            return Json(cities);
        }

        // Endpoint to get the list of suburbs by city id
        [HttpGet]
        public IActionResult GetSuburb(int id)
        {
            var suburbs = Suburbs.Where(suburb => suburb.CityID == id).ToList();
            return Json(suburbs);
        }
        public IActionResult ViewPatientHistory()
        {
            return View();
        }
        public IActionResult CapturePatientInfo()
        {
            return View();
        }
  
        [HttpPost]
        public ActionResult SubmitPatientInfo(IFormCollection form)
        {
            // Process and save patient information here

            // Redirect back to the index page
            return RedirectToAction("ViewAdmittedPatients");
        }
        public ActionResult RecaptureVitals()
        {
            //var vitals = new Vitals
            //{
            //    Temperature = 28.5,
            //    BloodPressureSystolic = 119,
            //    BloodPressureDiastolic = 70,
            //    ResRate = 15,
            //    HeartRate = 80,
            //    BloodOxygen = 97,
            //    BloodGlucoseLevel = 90
            //};
            //ViewBag.VitalsData = JsonConvert.SerializeObject(vitals);
            return View(Patients);
        }
        public IActionResult PreviewEmail()
        {
            //var vitals = new Vitals
            //{
            //    Temperature = 28.5,
            //    BloodPressureSystolic = 119,
            //    BloodPressureDiastolic = 70,
            //    ResRate = 15,
            //    HeartRate = 80,
            //    BloodOxygen = 97,
            //    BloodGlucoseLevel = 90
            //};
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string notes, string chartImageUrl)
        {
            // Logic to send email with notes and vitals chart as an attachment
            // Once email is sent, return success message
            ViewBag.Message = "Email has been sent successfully!";

            return View();
        }


        [HttpGet]
        public IActionResult AdmitPatient(int id)
        {
            var admittedPatient = _context.AdmittedPatients.Find(id);
            if (admittedPatient == null)
            {
                return NotFound();
            }

            var patient = _context.PatientUsers
                .SingleOrDefault(p => p.Id.ToString() == admittedPatient.PatientID);
            if (patient == null)
            {
                return NotFound();
            }

            ViewBag.Wards = _context.Wards.ToList();
            ViewBag.Beds = _context.Beds.Where(b => b.Status == "Available").ToList();
            ViewBag.AdmittedPatientId = admittedPatient.Id;

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdmittedPatientBed(int id, int bedId)
        {
            var admittedPatient = await _context.AdmittedPatients.FindAsync(id);
            if (admittedPatient == null)
            {
                return NotFound();
            }

            // Validate wardId and bedId against your business logic if needed

            admittedPatient.BedID = bedId;
            // Assuming you have WardID in AdmittedPatient model

            _context.AdmittedPatients.Update(admittedPatient);
            await _context.SaveChangesAsync();


            return RedirectToAction("CapturePatientInfo", new { id = admittedPatient.PatientID });
        }

        [HttpPost]
        public IActionResult SavePatientInfo(PatientUser patient)
        {
            var existingPatient = _context.PatientUsers.SingleOrDefault(p => p.IDNumber == patient.IDNumber);
            if (existingPatient == null)
            {
                return NotFound();
            }

            existingPatient.Email = patient.Email;
            existingPatient.PhoneNumber = patient.PhoneNumber;
            existingPatient.AddressLine1 = patient.AddressLine1;
            existingPatient.DateOfBirth = patient.DateOfBirth;

            _context.SaveChanges();

            ViewBag.Message = "Patient information updated successfully!";
            ViewBag.Wards = _context.Wards.ToList();
            ViewBag.Beds = _context.Beds.Where(b => b.Status == "Available").ToList();

            return View("AdmitPatient", existingPatient);
        }

        [HttpGet]
        public JsonResult GetBedsByWard(int wardId)
        {
            var beds = _context.Beds.Where(b => b.WardId == wardId && b.Status == "Available").ToList();
            return Json(beds);
        }
        public IActionResult EditPatient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditPatient(Dictionary<string, List<string>> formData)
        {
            // Process the form data, update the patient information in the database, etc.
            // For demonstration purposes, let's just return a success message
            return Ok("Patient information updated successfully");
        }


        
        public IActionResult ViewPatients()
        {
            return View();
        }

        public IActionResult ViewAdmittedPatients()
        {
            return View();
        }
 
       

        public ActionResult ReceiveMedication()
        {

            ReceiveMedicationVM viewModel = new ReceiveMedicationVM
            {
                ReceivedStatus = "Not Received", // Initial status
                PatientName = "Jade Appels",// Example patient name
                PatientID = 1, // Example patient ID
            };
            return View(viewModel);
        }

        public IActionResult PendingPrescriptions()
        {
            return View();
        }
        public ActionResult ReceivedPrescriptions()
        {


            // Pass the received prescriptions to the view
            return View(receivedPrescriptions);
        }
        public IActionResult AdministeredMedication()
        {
            return View();
        }

        public IActionResult PrescriptionDetails(int prescriptionId)
        {
            // Here, you would retrieve prescription details from your data source using the prescriptionId
            // For demonstration, let's create a dummy PrescriptionDetailsViewModel
            var viewModel = new PrescriptionDetailsViewModel
            {
                PrescriptionId = prescriptionId,
                PatientName = "Jade Appels",
                PatientID = "01091052229081",
                PharmacistName = "Mr. Goldman",
                PrescriptionDate = DateTime.Now,
                Medications = new List<MedicationVM>
            {
                new MedicationVM { MedicationName = "Panado", Frequency = "Twice a day", Quantity = 2 },
                new MedicationVM { MedicationName = "Alcougholex", Frequency = "Twice a day", Quantity = 1 },
                new MedicationVM { MedicationName = "Pretizones", Frequency = "Four times a day", Quantity = 6 }
            }
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AdministerMedication(int prescriptionId)
        {
            // Logic to administer medication for the given prescription ID
            // This could involve updating the database or performing any other necessary actions
            // For demonstration purposes, let's assume the medication was successfully administered
            var success = true;

            // Return a JSON result indicating the success status
            return Json(new { success });
        }
        [HttpGet]
        public JsonResult PatientHistory(string ID)
        {
            if (ID == "0109105229081")
            {
                List<string> s = ["Peniciline", "Panado", "Asprine"];
                List<string> c = ["Diabetic", "Cancer", "Lorerm"];
                List<string> m = ["Morphine (Pill)", "Iron Supplements (Pill)", "Aspirin"];
                PatientUser user = new()
                {
                    IDNumber = ID,
                    Email = "jfappels@gmail.com",
                    PhoneNumber = "0829607186",
                    Gender = "Male",
                    DateOfBirth = DateTime.Now,
                    AddressLine1 = "street",
                    AddressLine2 = "PE",
                    Name = "Jade Appels"
                };
                //Vitals vitals = new()
                //{
                //    Height = 122,
                //    Weight = 90,
                //    Temperature = 26,
                //    BloodGlucoseLevel = 5,
                //    BloodOxygen = 80,
                //    ResRate = 120,
                //    BloodPressureDiastolic = 80,
                //    BloodPressureSystolic = 120,
                //    HeartRate = 60,
                //    Date = DateTime.Now,
                //};
                PatientHistoryVM x = new()
                {
                    Appointments = d,
                    Allergies = s,
                    Prescription = ps,
                    //Vitals = vitals,
                    Patient = user,
                    Success = true,
                    Medication = m,
                    Condition = c,
                };
                return Json(x);
            }
            return Json(null);
        }
        [HttpPost]
        public JsonResult ReceiveMedication(int patientId)
        {
            bool success = true;

            return Json(new { success = success });
        }
    }
}


