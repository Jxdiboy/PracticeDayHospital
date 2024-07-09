using DayHospital.Data;
using DayHospital.Data.Email;
using DayHospital.Data.ViewModel;
using DayHospital.Models;
using DayHospital.Models.Nurse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection.Emit;
using DayHospital.Models.Admin;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DayHospital.Data.Static;
using System.Net;

namespace DayHospital.Controllers
{
    public class SurgeonController(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider) : Controller
    {
        private ApplicationDbContext _context = dbContext;
        private IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly ICompositeViewEngine _viewEngine = viewEngine;
        private readonly ITempDataProvider _tempDataProvider = tempDataProvider;
        //there double classess when it comes to medicines
        
        static PatientUser user = new()
        {
            Name = "John Dou",
            Email = "johndou@gmail.com",
            Gender = "Male",
            PhoneNumber = "1234567890",
            IDNumber = "1234567891234",
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
        static List<Province> provinces = [
            new()
                {
                    Id = 1,
                    Name = "Gauteng"
                },
            new(){
                Id = 2,
                Name = "Free State"
            }
            ];
        static List<City> cities = [
            new(){
                ProvinceID = 1,
                Name = "Jozi",
                Id = 1
            },
            new(){
                ProvinceID = 1,
                Name = "Pta",
                Id = 2
            },
            new(){
                ProvinceID = 2,
                Name = "Bloem",
                Id = 3
            },
            new(){
                ProvinceID = 2,
                Name = "Mafikeng",
                Id = 4
            },
            ];
        static List<Suburb> suburbs = [
            new(){
                Id = 1,
                CityID = 1,
                Name ="Midrand"
            },
            new(){
                Id = 2,
                CityID = 1,
                Name ="New Town"
            },
            new(){
                Id = 3,
                CityID = 2,
                Name ="Sam"
            },
            new(){
                Id = 4,
                CityID = 2,
                Name ="New Hope"
            },
            new(){
                Id = 5,
                CityID = 3,
                Name ="Some town"
            },
            new(){
                Id = 6,
                CityID = 4,
                Name ="New Hope only"
            },
            ];
        public async Task<IActionResult> Pdf()
        {
            BookingAppointmentVM2 surgery = new()
            {
                Patient = user,
                Theater = "Hello World",
                Anae = "DR Fred",
                Date = DateTime.Now,
                Codes =
                [
                    new() {
                        TreatmentId = "T12-9",
                        TreatmentName = "Teeth Removal"
                    },
                    new(){
                        TreatmentName = "Stiches",
                        TreatmentId = "KL98-7"

                    }
                ],
                Bed = "3",
                Ward = "W",
                Id = 1,
                Time = "Afternoon"

            };
            ////Render Razor page to HTML
            //var htmlContent = await RenderViewToString("ViewAppointment", surgery);
            //if (string.IsNullOrEmpty(htmlContent))
            //{
            //    return NotFound();
            //}
            try
            {
                // Convert HTML to PDF using Rotativa
                var pdfBytes = await new ViewAsPdf("ViewAppointment1", surgery)
                {
                    //https://www.nuget.org/packages/Rotativa.AspNetCore
                    //https://wkhtmltopdf.org/downloads.html
                    FileName = "Report.pdf",
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageMargins = new Rotativa.AspNetCore.Options.Margins(5, 5, 5, 5),
                    IsJavaScriptDisabled = false,
                    // Additional options like PageMargins, CustomSwitches, etc. can be specified here
                }.BuildFile(ControllerContext);


                // Return the PDF file as a download
                return File(pdfBytes, "application/pdf", "Report.pdf");
            }
            catch
            {
                TempData["error"] = "Sorry. Something went wrong while generating the PDF.";
                return RedirectToAction(nameof(Index));
            }
            
        }

        // Helper method to render Razor view to string
        public async Task<string> RenderViewToString(string viewName, object model = null)
        {
            var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

            if (viewResult.View == null)
            {
                return "";
            }

            using var sw = new StringWriter();
            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
            var viewContext = new ViewContext(ControllerContext, viewResult.View, viewDictionary, new TempDataDictionary(ControllerContext.HttpContext, _tempDataProvider), sw, new HtmlHelperOptions());
            await viewResult.View.RenderAsync(viewContext);
            return sw.GetStringBuilder().ToString();
        }
        public async Task<IActionResult> Index()
        {
            var beds = _context.Beds;
            var ward = _context.Wards;
            var Theater = _context.Theaters;
            var room = (from b in beds
                        join w in ward on b.WardId equals w.Id
                        select new
                        {
                            Bed = b.RoomNumber,
                            Ward = w.Name,
                            b.Id
                        }).AsEnumerable();

            AppointmentStats stat= new();

            var p = await _context.Prescriptions.Where(x => x.DateTime.Date == DateTime.Now.Date && x.SurgeonId == HttpContext.Session.GetString("id") && x.Status == "Rejected" && x.IsNew == false).ToListAsync();
            var pat = _context.PatientUsers;
            var d = _context.ApplicationUsers;
            var r = (from pres in p
                     join user in pat on pres.PatientId equals user.IDNumber into table1
                     from user in table1.ToList()
                     join doc in d on pres.PharamID equals doc.IDNumber into table2
                     from doc in table2.ToList()
                     select new PrescriptionSD
                     {
                         Patient = $"{user.Name} {user.Surname}",
                         PatientId = user.IDNumber,
                         Priority = pres.Priority,
                         Date = pres.DateTime.ToShortDateString(),
                         Reason = pres.RejectReason == null ? "Error. Reason not found" : pres.RejectReason.Length > 30 ? $"{pres.RejectReason[..30]} ..." : pres.RejectReason,
                         FullReason = pres.RejectReason == null ? "Error. Reason not found" : pres.RejectReason,
                         Phrama = $"{doc.Name} {doc.Surname}",
                         PresID = pres.Id
                     }).DistinctBy(x=>x.PresID).AsEnumerable();

            var book = _context.SurgeryBookings;
            var discharge = _context.SurgeryBookings.Where(x=> x.IsConfirmed == false && x.Date == DateTime.Now.Date && x.Status != "Deleted" && x.SurgeonId == HttpContext.Session.GetString("id")).ToList();
            var discharge1 = _context.SurgeryBookings.Where(x=> x.IsConfirmed == true && x.Date == DateTime.Now.Date && x.IsDone == false && x.Status != "Deleted" && x.SurgeonId == HttpContext.Session.GetString("id")).ToList();
            var admitted = _context.AdmittedPatients;
            stat.Discharged = book.Where(x => x.Date.Date == DateTime.Now.Date && x.IsDone && x.IsConfirmed).Count();
            stat.Admitted = await (from f in admitted
                             join b in book on f.BookingID equals b.Id
                             where f.DateTime.Date == DateTime.Now.Date
                             where b.IsConfirmed == true
                             where b.IsDone == false
                             select f).CountAsync();
            stat.LateArrivals = book.Where(x => x.Date.Date == DateTime.Now.Date && !x.IsDone && !x.IsConfirmed).Count();
            stat.Total = stat.LateArrivals + stat.Admitted + stat.Discharged;
            var d1 = (from Dis in discharge1
                      join Pat in pat on Dis.PatientIDNumber equals Pat.IDNumber into table1
                      from Dis1 in table1.AsEnumerable()
                      join admit in admitted on Dis.Id equals admit.BookingID into table2
                      from admit1 in table2.AsEnumerable()
                      join bed in room on admit1.BedID equals bed.Id into table3
                      from bed in table3.AsEnumerable()
                      select new DischargeSD
                      {
                          PatId = Dis1.IDNumber,
                          BookingId = Dis.Id,
                          Patient = $"{Dis1.Name} {Dis1.Surname}",
                          Gender = Dis1.Gender,
                          Ward = bed.Ward,
                          Bed = bed.Bed,
                      }).DistinctBy(x => x.BookingId).AsEnumerable();
            var nurse = _context.ApplicationUsers.Where(x=>x.Role == Roles.Nurse).ToList();

            var d11 = (from Dis in discharge
                       join Pat in pat on Dis.PatientIDNumber equals Pat.IDNumber into table1
                     from Dis1 in table1.Distinct().ToList()
                     join admit1 in admitted on Dis.Id equals admit1.BookingID into table2
                     join thea in Theater on Dis.TheaterId equals thea.Id
                     from admit2 in table2.Distinct().ToList()
                     join nur in nurse on admit2.NurseId equals nur.IDNumber
                     join bed in room on admit2.BedID equals bed.Id into table3
                     from bed in table3.Distinct().ToList()
                    select new AdmittedPatVMSD
                    {
                        PatId = Dis1.IDNumber,
                        BookingId = Dis.Id,
                        Patient = $"{Dis1.Name} {Dis1.Surname}",
                        Gender = Dis1.Gender,
                        Ward = bed.Ward,
                        Bed = bed.Bed,
                        NurseName = $"{nur.Name} {nur.Surname}",
                        DateWithSession = $"{Dis.Session}",
                        Theater = thea.Name,
                    }).DistinctBy(x=>x.BookingId).AsEnumerable(); // add distinct just in case


            SurgeonDashboard s = new()//Viewmodel
            {
                Prescriptions = r,
                Discharges = d1,
                AdmittedPats = d11,
                Stats = stat
            };
            return View(s);
        }
        public async Task<IActionResult> Data123()
        {
            StreamReader st = new StreamReader("meds.txt");
            List<Medicine> x = new List<Medicine>();

            while (!st.EndOfStream)
            {
                string s = st.ReadLine().Replace("\"","");
                Random r = new();
                if (s != null)
                {

                    Medicine m = new()
                    {
                        Name = s,
                        ScheduleLevelId = r.Next(1, 7),
                        Description = s,
                        DosageId = r.Next(1, 4),
                        Quantity = r.Next(100, 400),
                        ReOrderLevel = 100

                    };
                    x.Add(m);

                }
            }
            await _context.Medicines.AddRangeAsync(x);
            await _context.SaveChangesAsync();
            TempData["pass"] = "Added new data";

            return View(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> Discharge(string? note,int? id, string? time)
        {
            if (string.IsNullOrEmpty(note) || id == null || string.IsNullOrEmpty(time))
            {
                return Json(false);
            }
            if (DateTime.TryParse($"{DateTime.Now.ToShortDateString()} {time}", out DateTime dt))
            {
                Discharge x = new()
                {
                    AdmittedId = (int)id,
                    SurgeonNote = note,
                    SurgeonDateTime = dt,
                };
                await _context.Discharges.AddAsync(x);
                await _context.SaveChangesAsync();

                var booking = _context.SurgeryBookings.Where(x=>x.Id == id).FirstOrDefault();
                if (booking == null)
                {
                    return Json(false);
                }
                booking.IsDone = true;
                _context.SurgeryBookings.Update(booking);
                await _context.SaveChangesAsync();
                return Json(true);

            }
            return Json(false);
        }
        
        public static List<string> ConvertQE(IEnumerable<ListStrings> q)
        {
            List<string> listA = new();

            if (q != null && q.Count() > 0)
            {
                foreach (var item in q)
                {
                    listA.Add(item.Name);
                }
            }
            return listA;
        }

        [HttpGet]
        public JsonResult MedicationInfo(int? id)//
        {
            if (id == null)
            {
                return Json(null);
            }
            var m = _context.Medicines.Where(x => x.Id == id).FirstOrDefault();
            if (m == null)
            {
                return Json(null);

            }
            var dosage = _context.Dosages.Where(x => x.Id == m.DosageId).First().Name;
            var s = _context.MedicineSchedules.Where(x => x.Id == m.ScheduleLevelId).FirstOrDefault();

            var mai = _context.MedicineActiveIngredients.Where(x => x.MedicineId == m.Id).AsEnumerable();
            var a = _context.ActiveIngredients;
            var ai = (from act in a
                     join med in mai on act.Id equals med.ActiveIngredientId
                     select new ListStrings { Name = $"{act.Name} ({med.Strength} ml)" }).Distinct().AsEnumerable();//

            var medind = _context.MedicationIndications.Where(x => x.MedicationId == m.Id).AsEnumerable();
            var ind = _context.Indications;
            var j = (from  l in medind
                    join i in ind on l.IndicationId equals i.Id
                    select new ListStrings { Name = i.Name }).Distinct().AsEnumerable();

            var con = _context.ChronicConditions;
            var ind1 = _context.ContraIndications;
            var j1 = (from c in con 
                      join i in ind1 on c.Id equals i.CondtionID into table1
                      from i in table1.AsEnumerable()
                      join q in mai on i.ActiveIngredientId equals q.ActiveIngredientId
                     select new ListStrings { Name = c.Description }).Distinct().AsEnumerable();

            var inter = _context.MedicineInteractions;
            List<int> list = [];
            /*list all the active ing associsated with that medicine then
             *then filter till we get the other medicine (interaction)
             *then get thier names
             */
            foreach (var i in mai)
            {
                List<string> temp = [];
                var v = inter.Where(x => (x.ActiveIngredient1 == i.ActiveIngredientId) || (x.ActiveIngredient2 == i.ActiveIngredientId)).AsEnumerable();
                if (v != null)
                {
                    foreach (var item in inter)
                    {
                        if (item.ActiveIngredient1 == i.ActiveIngredientId)
                        {
                            list.Add(item.ActiveIngredient2);
                        }
                        if (item.ActiveIngredient2 == i.ActiveIngredientId)
                        {
                            list.Add(item.ActiveIngredient1);
                        }


                    }
                }
            }
            var last = a.Where(x => list.Contains(x.Id)).Select(x => new ListStrings { Name = x.Name });
            var x1 = new MedicationInfoVM()
            {
                Name = $"{m.Name}",
                Dosage = dosage,
                Quantity = m.Quantity,
                Schedulelevel = s == null ? "Error" : $"{s.Level}",
                ActiveIn = ConvertQE(ai),
                Indication = ConvertQE(j),
                ContraInd = ConvertQE(j1),
                Interaction = ConvertQE(last),


            };
            return Json(x1);
        }
        [HttpGet]
        public JsonResult GetProvince()
        {
            //var list = _context.Provinces;
            
            return Json(provinces);
        }
        [HttpGet]
        public JsonResult GetCity(string id)
        {
            //var list = _context.Cities.Where(x => x.ProvinceID == int.Parse(id));
            var list = cities.Where(x => x.ProvinceID == int.Parse(id));
            return Json(list);
        }
        [HttpGet]
        public JsonResult GetSuburb(string id)
        {
            //var list = _context.Suburbs.Where(x => x.CityID == int.Parse(id));
            var list = suburbs.Where(x => x.CityID == int.Parse(id));
            return Json(list);
        }
        [HttpGet]
        public JsonResult GetIndications()
        {
            var list = _context.Indications.AsEnumerable();
            return Json(list);
        }
        [HttpGet]
        public JsonResult GetContraIndications()
        {
            var list = _context.ChronicConditions.AsEnumerable();
            return Json(list);
        }
        [HttpGet]
        public JsonResult SearchMedName(string? med)
        {
            if (med==null)
            {
                return Json(null);
            }
            var list = _context.Medicines.Where(x => x.Name.Contains(med)).AsEnumerable();
            var dos = _context.Dosages;
            var list1 = from m in list
                       join d in dos on m.DosageId equals d.Id
                       select new
                       {
                           Name = $"{m.Name} ({d.Name})",
                           Id = m.Id,
                       };
            return Json(list1);
        }

        [HttpPost]
        public JsonResult GetMedInd([FromBody] List<Indication> list)
        {
            if (list != null)
            {
                var medInd = _context.MedicationIndications;
                var med = _context.Medicines;

                var i1 = (from m in list 
                         join md in medInd on m.Id equals md.IndicationId
                         select new
                         {
                             MedId = md.MedicationId,
                         }
                    ).Distinct().AsEnumerable();
                var i2 = (from i in i1 
                          join m in med on i.MedId equals m.Id
                          select new
                          {
                              Name = $"{m.Name}"
                          }).Distinct().AsEnumerable();
                return Json(i2);
            }

            return Json(null);
        }
        [HttpPost]
        public JsonResult GetMedConInd([FromBody] List<Indication> list)
        {
            if (list != null)
            {
                var medCon = _context.ContraIndications;
                var med = _context.Medicines;
                var act = _context.ActiveIngredients;
                var medI = _context.MedicineActiveIngredients;

                var model = (from l in list 
                             join m in medCon on l.Id equals m.CondtionID
                             select new
                             {
                                 id = m.ActiveIngredientId
                             }).AsEnumerable();
                
                var m2 = (from m in model
                         join a in act on m.id equals a.Id
                         select new
                         {
                             id = a.Id
                         }).AsEnumerable();
                var m3 = (from m in m2
                          join z in medI on m.id equals z.ActiveIngredientId
                          select new
                          {
                              id = z.MedicineId
                          }).AsEnumerable();
                var m4 = (from m in m3
                          join z in med on m.id equals z.Id
                          select new
                          {
                              z.Name
                          }).Distinct().AsEnumerable();


                return Json(m4);
            }

            return Json(null);
        }

        [HttpPost]
        public async Task<JsonResult> AddPatient([FromBody] PatientUser patient)
        {
            JSMessage jS;
            
            if (ModelState.IsValid)
            {
                var pat = await _context.PatientUsers.Where(x=>x.IDNumber == patient.IDNumber).FirstOrDefaultAsync();
                if (pat != null)
                {
                    jS = new()
                    {
                        Success = false,
                        Message = "ID Number is already in the system"
                    };
                    return Json(jS);
                }
                
                await _context.PatientUsers.AddAsync(patient);
                await _context.SaveChangesAsync();
                jS = new()
                {
                    Success = true,
                    Message = $"Successfully Added {patient.Name}"
                };
                return Json(jS);
            }
            jS = new()
            {
                Success = false,
                Message = "There is Missing Data"
            };
            return Json(jS);
        }
        [HttpPost]
        public async Task<JsonResult> NotifyNurse([FromBody] ExtraReading extra)
        {
            JSMessage jS;
            
            if (ModelState.IsValid)
            {
                extra.Vitals = extra.Vitals[..^1];
                await _context.ExtraReadings.AddAsync(extra);
                await _context.SaveChangesAsync();
                jS = new()
                {
                    Success = true,
                    Message = $"Success."
                };
                return Json(jS);
            }
            jS = new()
            {
                Success = false,
                Message = "There is Missing Data"
            };
            return Json(jS);
        }

        public IActionResult BookAppointment() 
        {
            LoadDropdownAppointment();

            return View();
        }
        [HttpPost]
        public async Task<JsonResult> UpdateAppointment([FromBody] BookingAppointmentVM booking, int? Id)
        {
            JSMessage jS = new();
            if (Id == null)
            {
                jS = new()
                {
                    Success = false,
                    Message = "Invalid Data sent."
                };
                return Json(jS);
            }
            if (ModelState.IsValid)
            {
                if (DateTime.TryParse($"{booking.Date}", out DateTime dt))
                {
                    if (dt.Date < DateTime.Now.Date)
                    {
                        jS.Success = false;
                        jS.Message = "Incorrect Date";
                        return Json(jS);
                    }
                    var z = await _context.SurgeryBookings.Where(x => x.Id == Id).FirstAsync();
                    if (z == null)
                    {
                        jS = new()
                        {
                            Success = false,
                            Message = "Invalid booking id."
                        };
                        return Json(jS);
                    }
                    z.AnaeId = booking.AnaeId;
                    z.TheaterId = booking.TheaterId;
                    z.Date = dt;
                    z.Session = booking.Time;
                    _context.SurgeryBookings.Update(z);
                    List<SurgeryBookingTreatmentCode> surgeries = new();
                    foreach (var item in booking.Codes)
                    {
                        SurgeryBookingTreatmentCode x = new()
                        {
                            BookingId = z.Id,
                            TreatmentCode = item.TreatmentId,
                        };
                        surgeries.Add(x);
                    }
                    _context.SurgeryBookingTreatmentCodes.RemoveRange(_context.SurgeryBookingTreatmentCodes.Where(x => x.BookingId == z.Id).AsEnumerable());
                    await _context.SaveChangesAsync();

                    await _context.SurgeryBookingTreatmentCodes.AddRangeAsync(surgeries);
                    await _context.SaveChangesAsync();
                    jS = new()
                    {
                        Success = true,
                        Message = "Successfully Updated Appointment"
                    };
                    return Json(jS);
                }

            }
            jS = new()
            {
                Success = false,
                Message = "There is Missing Data"
            };
            return Json(jS);
        }
        public IActionResult Appointments()
        {
            var Pat = _context.PatientUsers;
            var Theater = _context.Theaters;
            var booking = _context.SurgeryBookings.Where(x=>x.Date.Date ==  DateTime.Now.Date && x.SurgeonId == HttpContext.Session.GetString("id") && x.Status != "Deleted").AsEnumerable();
            var m = (from B in booking
                    join P in Pat on B.PatientIDNumber equals P.IDNumber into table1
                    from B1 in table1.ToList()
                    join T in Theater on B.TheaterId equals T.Id
                    select new BookingList
                    {
                        PatId = B1.IDNumber,
                        Patient = $"{B1.Name} {B1.Surname}",
                        Theater = T.Name,
                        Date = B.Date.ToShortDateString(),
                        Time = B.Session,
                        IsConfirmed = B.IsConfirmed,
                        BookingId = B.Id,
                        Status = B.Status,
                    }).DistinctBy(x => x.BookingId).AsEnumerable();
            return View(m);
        }
        [HttpPost]
        public async Task<JsonResult> UpdatePrescription([FromBody] PrescriptionVM prescription, string? Id)
        {
            JSMessage jS;
            if (Id == null)
            {
                jS = new()
                {
                    Success = false,
                    Message = "Invalid Data sent. Please refresh the page."
                };
                return Json(jS);
            }
            if (ModelState.IsValid)
            {
                if (DateTime.TryParse(prescription.Date, out DateTime dt))
                {
                    var d = _context.Prescriptions.Where(x => x.Id == int.Parse(Id) && x.SurgeonId == prescription.SurgeonId).FirstOrDefault();
                    if (d == null)
                    {
                        jS = new()
                        {
                            Success = false,
                            Message = "Invalid prescription id"
                        };
                        return Json(jS);
                    }
                    d.Priority = prescription.Priority;
                    d.DateTime = dt;

                    _context.Prescriptions.Update(d);
                    _context.SaveChanges();


                    _context.PrescriptionMedicines.RemoveRange(_context.PrescriptionMedicines.Where(x => x.PrescriptionId == d.Id).AsEnumerable());
                    await _context.SaveChangesAsync();

                    prescription.Medicine.ForEach(m => m.PrescriptionId = d.Id);
                    prescription.Medicine.ForEach(m => m.Id = 0);
                    _context.PrescriptionMedicines.AddRange(prescription.Medicine);
                    await _context.SaveChangesAsync();
                    
                    jS = new()
                    {
                        Success = true,
                        Message = "Successfully Updated Prescription"
                    };
                }
                else
                {
                    jS = new()
                    {
                        Success = false,
                        Message = "Invalid Date entry"
                    };
                }
                return Json(jS);


            }
            jS = new()
            {
                Success = false,
                Message = "There is Missing Data"
            };
            return Json(jS);
        }
        public async Task<IActionResult> ViewPrescription(int? ID)
        {
            if (ID == null || ID <= 0)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Prescription));
            }
            try
            {
                var pres = await _context.Prescriptions.Where(x => x.Id == ID && x.SurgeonId == HttpContext.Session.GetString("id")).FirstAsync();
                var user = await _context.PatientUsers.Where(x => x.IDNumber == pres.PatientId).FirstAsync();
                var meds = await _context.PrescriptionMedicines.Where(x => x.PrescriptionId == pres.Id).ToListAsync();
                var phara = await _context.ApplicationUsers.Where(x=>x.IDNumber == pres.PharamID).FirstOrDefaultAsync();
                if (pres == null || user == null || meds == null)
                {
                    TempData["error"] = "Sorry. Could not find record";
                    return RedirectToAction(nameof(Prescription));
                }
                PrescriptionVM p = new()
                {
                    PatientId = pres.PatientId,
                    Date = pres.DateTime.ToShortDateString(),
                    Priority = pres.Priority,
                    Status = pres.Status,
                    Medicine = meds,
                    User = user,
                    Reason = pres.RejectReason,
                    Pharama = phara == null ? "User not found." : $"{phara.Name} {phara.Surname}",
                };
                return View(p);
            }
            catch
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Prescription));
            }
        }
        public async Task<IActionResult> EditAppointment(int? Id)
        {
            LoadDropdownAppointment();

            if (Id == null)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Appointments));
            }
            var m = await _context.SurgeryBookings.Where(x=>x.Id == Id && x.SurgeonId == HttpContext.Session.GetString("id") && x.IsConfirmed == false).FirstOrDefaultAsync();
            if (m == null)
            {
                TempData["error"] = "Sorry. Booking as been Confirmed";
                return RedirectToAction(nameof(Appointments));
            }
            var x = new { Id = Id.ToString() };
            return View(x);
        }
        public async Task<IActionResult> DeleteAppointment(int? Id)
        {
            if (Id == null || Id <= 0)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Appointments));
            }
            var m = await _context.SurgeryBookings.Where(x => x.Id == Id && x.SurgeonId == HttpContext.Session.GetString("id")).FirstOrDefaultAsync();
            if (m == null)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Appointments));
            }
            m.Status = "Deleted";
            _context.SurgeryBookings.Update(m);
            await _context.SaveChangesAsync();
            TempData["pass"] = "Booking successfully deleted";
            return RedirectToAction(nameof(Appointments));
        } 
        public async Task<IActionResult> EditPrescription(int? Id)
        {
            LoadDropdownPres();
            if (Id == null || Id <= 0)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Prescription));
            }
            var m = await _context.Prescriptions.Where(x => x.Id == Id && x.SurgeonId == HttpContext.Session.GetString("id")).FirstOrDefaultAsync();
            if (m == null)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Prescription));
            }
            var x = new { Id = m.Id.ToString(), PatID = m.PatientId };
            return View(x);
        }
        public async Task<IActionResult> DeletePrescription(int? Id)
        {
            if (Id == null || Id <= 0)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Prescription));
            }
            var m = await _context.Prescriptions.Where(x => x.Id == Id && x.SurgeonId == HttpContext.Session.GetString("id")).FirstOrDefaultAsync();
            if (m == null)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Prescription));
            }
            m.Status = "Deleted";
            _context.Prescriptions.Update(m);
            await _context.SaveChangesAsync();
            TempData["pass"] = "Prescription has been deleted";
            return RedirectToAction(nameof(Prescription));
        }
        [HttpPost]
        public async Task<JsonResult> SearchPrescription(string? Id)
        {
            if (Id == null)
            {
                return Json(null);
            }

            var pres = _context.Prescriptions.Where(x => x.Id == int.Parse(Id)).FirstOrDefault();
            if (pres == null)
            {
                return Json(null);
            }
            var bCodes = _context.TreatmentCodes;
            var c1 = _context.ChronicConditions;
            var a1 = _context.ActiveIngredients;
            var m1 = _context.Medicines;
            var pc1 = _context.PatientChronicConditions.Where(x => x.PatientID == pres.PatientId).AsEnumerable();
            var pm1 = _context.PatientCurrentMedications.Where(x => x.PatientID == pres.PatientId).AsEnumerable();
            var pa1 = _context.Allergies.Where(x => x.PatientId == pres.PatientId).AsEnumerable();

            var mc1 = from l in c1
                      join k in pc1 on l.Id equals k.ConditionID
                      select new ListStrings
                      {
                          Name = l.Description
                      };
            var ma1 = from l in a1
                      join k in pa1 on l.Id equals k.ActiveIngredientId
                      select new ListStrings
                      {
                          Name = l.Name
                      };
            var mm1 = from l in m1
                      join k in pm1 on l.Id equals k.MedicineID
                      select new ListStrings
                      {
                          Name = l.Name
                      };

            //List<string> m = ["Peniciline", "Panado", "Asprine"];
            //List<string> c = ["Diabetic", "Cancer", "Lorerm"];
            //List<string> a = ["Morphine (Pill)", "Iron Supplements (Pill)", "Aspirin"];
            var date = new List<string>()
                {
                    "10 may 2024 12:00",
                    "10 may 2024 12:30",
                    "10 may 2024 13:30",
                    "10 may 2024 15:00",
                    "10 may 2024 15:30",
                    "10 may 2024 16:00",
                };
            var hr = new List<string>()
                {
                    "60",
                    "69",
                    "50",
                    "55",
                    "70",
                    "85",
                };
            var bps = new List<string>()
                {
                    "100",
                    "120",
                    "130",
                    "140",
                    "120",
                    "125",
                };
            var bpd = new List<string>()
                {
                    "80",
                    "84",
                    "84",
                    "90",
                    "85",
                    "81",
                };

            var bgl = new List<string>()
                {
                    "70",
                    "89",
                    "90",
                    "55",
                    "70",
                    "85",
                };
            var bol = new List<string>()
                {
                    "60",
                    "69",
                    "50",
                    "55",
                    "70",
                    "85",
                };
            var rr = new List<string>()
                {
                    "6",
                    "10",
                    "19",
                    "5",
                    "14",
                    "12",
                };

            var g = new { hr = new { date, hr }, rr = new { date, rr }, bol = new { date, bol }, bgl = new { date, bgl }, bp = new { date, bps, bpd } };
            var medicines = _context.PrescriptionMedicines.Where(x => x.PrescriptionId == pres.Id).AsEnumerable(); //is needed
            if (medicines == null)
            {
                return Json(null);
            }
            var booking = _context.SurgeryBookings.Where(x => x.PatientIDNumber == pres.PatientId).OrderByDescending(x => x.Date.Date).FirstOrDefault();
            if (booking != null)
            {
                var pbcodes = _context.SurgeryBookingTreatmentCodes.Where(x => x.BookingId == booking.Id).AsEnumerable();
                var codes = (from co in pbcodes
                             join bo in bCodes on co.TreatmentCode equals bo.TreatmentId
                             select bo).AsEnumerable();
                var user = _context.PatientUsers.Where(x => x.IDNumber == pres.PatientId).FirstOrDefault();
                if (user == null)
                {
                    return Json(null);
                }
                var theater = _context.Theaters.Where(x => x.Id == booking.TheaterId).First().Name;
                //var Anae = _context.ApplicationUsers.Where(x => x.IDNumber == booking.AnaeId).First().Name;
                var admit = _context.AdmittedPatients.Where(x => x.BookingID == booking.Id).FirstOrDefault();
                if (admit != null)
                {
                    var bed = _context.Beds.Where(x => x.Id == admit.BedID).FirstOrDefault();
                    var ward = _context.Wards.Where(x => x.Id == bed.WardId).FirstOrDefault();
                    BookingAppointmentVM2 surgery = new()
                    {
                        Patient = user,
                        Theater = theater,
                        Anae = "0",
                        Date = booking.Date.Date,
                        Codes = codes,
                        Bed = bed == null ? "Not found." : $"{bed.RoomNumber}",
                        Ward = ward == null ? "Not found." : $"{ward.Name}",
                        Id = 1,
                        Time = booking.Session,
                    };
                    var xx1 = new
                    {
                        SurgeonId = pres.SurgeonId,
                        PatientId = pres.PatientId,
                        Date = pres.DateTime.Date,
                        Priority = pres.Priority,
                        Medicine = medicines,
                        Patient = user,
                        Graph = g,
                        //just need to wprk on the graphs
                        History = new { a = ConvertQE(ma1), c = ConvertQE(mc1), m = ConvertQE(mm1) },
                        Booking = surgery,
                        bmi = new { w = admit.Weight, h = admit.Height, bmi = admit.BMI }
                    };
                    return Json(xx1);
                }
                BookingAppointmentVM2 surgery1 = new()
                {
                    Patient = user,
                    Theater = theater,
                    Anae = "0",
                    Date = booking.Date.Date,
                    Codes = codes,
                    Bed = null,
                    Ward = null,
                    Id = 1,
                    Time = booking.Session,
                };
                var xx11 = new
                {
                    SurgeonId = pres.SurgeonId,
                    PatientId = pres.PatientId,
                    Date = pres.DateTime.Date,
                    Priority = pres.Priority,
                    Medicine = medicines,
                    Patient = user,
                    Graph = g,
                    //just need to wprk on the graphs
                    History = new { a = ConvertQE(ma1), c = ConvertQE(mc1), m = ConvertQE(mm1) },
                    Booking = surgery1,
                };
                return Json(xx11);

            }
            

            
            
            var xx = new
            {
                SurgeonId = pres.SurgeonId,
                PatientId = pres.PatientId,
                Date = pres.DateTime.Date,
                Priority = pres.Priority,
                Medicine = medicines,
                Patient = user,
                Graph = g,
                //just need to wprk on the graphs
                History = new { a = ConvertQE(ma1), c = ConvertQE(mc1), m = ConvertQE(mm1) },
            };
            return Json(xx);
        }
        [HttpGet]
        public async Task<JsonResult> SearchAppointment(int? Id)
        {
            if (Id == null || Id <= 0)
            {
                return Json(null);
            }
            var app = _context.SurgeryBookings.Where(x => x.Id == Id && x.SurgeonId == HttpContext.Session.GetString("id")).FirstOrDefault();
            if (app == null)
            {
                return Json(null);
            }
            var user = _context.PatientUsers.Where(x => x.IDNumber == app.PatientIDNumber).FirstOrDefault();
            var tcodes = _context.TreatmentCodes;
            var c = _context.SurgeryBookingTreatmentCodes.Where(x => x.BookingId == app.Id).AsEnumerable();
            var codes1 = (from co in tcodes
                          join bo in c on co.TreatmentId equals bo.TreatmentCode
                          select co).AsEnumerable();
            var admit = _context.AdmittedPatients.Where(x => x.BookingID == app.Id).FirstOrDefault();
            if (app == null || user == null)
            {
                return Json(null);
            }
            if (admit == null)
            {
                //var x22 = new { Patient = user, Tcode = codes1, AnaeId = app.AnaeId.ToString(), Date = app.Date.ToShortDateString(), Time = app.Session, TheaterId = app.TheaterId };
                var x22 = new { Patient = user, Tcode = codes1, AnaeId = "1", Date = app.Date.ToShortDateString(), Time = app.Session, TheaterId = app.TheaterId };
                return Json(x22);
            }
            //var x2 = new { Patient = user, Tcode = codes1, AnaeId = app.AnaeId.ToString(), Date =  app.Date.ToShortDateString(), Time = app.Session, TheaterId = app.TheaterId, bmi = new { w = admit.Weight, h = admit.Height, bmi = admit.BMI } };
            var x2 = new { Patient = user, Tcode = codes1, AnaeId = "", Date =  app.Date.ToShortDateString(), Time = app.Session, TheaterId = app.TheaterId, bmi = new { w = admit.Weight, h = admit.Height, bmi = admit.BMI } };
            return Json(x2);
        }
        
        [HttpGet]
        public JsonResult SearchPatientB(string Id)
        {
            var user = _context.PatientUsers.Where(x => x.IDNumber == Id).FirstOrDefault();
            if (user == null)
            {
                return Json(null);
            }
            
            var x = new { p = user };
            return Json(x);
            
        }
        [HttpGet]
        public JsonResult SearchPatient(string Id)
        {
            var date = new List<string>()
                {
                    "10 may 2024 12:00",
                    "10 may 2024 12:30",
                    "10 may 2024 13:30",
                    "10 may 2024 15:00",
                    "10 may 2024 15:30",
                    "10 may 2024 16:00",
                };
            var hr = new List<string>()
                {
                    "60",
                    "69",
                    "50",
                    "55",
                    "70",
                    "85",
                };
            var bps = new List<string>()
                {
                    "100",
                    "120",
                    "130",
                    "140",
                    "120",
                    "125",
                };
            var bpd = new List<string>()
                {
                    "80",
                    "84",
                    "84",
                    "90",
                    "85",
                    "81",
                };

            var bgl = new List<string>()
                {
                    "70",
                    "89",
                    "90",
                    "55",
                    "70",
                    "85",
                };
            var bol = new List<string>()
                {
                    "60",
                    "69",
                    "50",
                    "55",
                    "70",
                    "85",
                };
            var rr = new List<string>()
                {
                    "6",
                    "10",
                    "19",
                    "5",
                    "14",
                    "12",
                };
            var user = _context.PatientUsers.Where(x => x.IDNumber == Id).FirstOrDefault();
            if (user == null)
            {
                return Json(null);
            }
            var bCodes = _context.TreatmentCodes;
            var c1 = _context.ChronicConditions;
            var a1 = _context.ActiveIngredients;
            var m1 = _context.Medicines;
            var pc1 = _context.PatientChronicConditions.Where(x => x.PatientID == user.IDNumber).AsEnumerable();
            var pm1 = _context.PatientCurrentMedications.Where(x => x.PatientID == user.IDNumber).AsEnumerable();
            var pa1 = _context.Allergies.Where(x => x.PatientId == user.IDNumber).AsEnumerable();

            var mc1 = (from l in c1
                      join k in pc1 on l.Id equals k.ConditionID
                      select new ListStrings
                      {
                          Name = l.Description
                      }).AsEnumerable();
            var ma1 = (from l in a1
                      join k in pa1 on l.Id equals k.ActiveIngredientId
                      select new ListStrings
                      {
                          Name = l.Name
                      }).AsEnumerable();
            var mm1 = (from l in m1
                      join k in pm1 on l.Id equals k.MedicineID
                      select new ListStrings
                      {
                          Name = l.Name
                      }).AsEnumerable();


            var app = _context.SurgeryBookings.Where(x => x.PatientIDNumber == user.IDNumber && x.Date.Date >= DateTime.Today.Date && x.Status != "Deleted").FirstOrDefault();
            if (app != null)
            {
                var tcodes = _context.TreatmentCodes;
                var c = _context.SurgeryBookingTreatmentCodes.Where(x => x.BookingId == app.Id).AsEnumerable();
                var codes1 = (from co in tcodes
                              join bo in c on co.TreatmentId equals bo.TreatmentCode
                              select co).ToList();
                var admit = _context.AdmittedPatients.Where(x => x.BookingID == app.Id).FirstOrDefault();
                var t = _context.Theaters.Where(x => x.Id == app.TheaterId).FirstOrDefault();

                if (admit != null)
                {
                    var bed = _context.Beds.Where(x => x.Id == admit.BedID).FirstOrDefault();
                    var ward = _context.Wards.Where(x => x.Id == bed.WardId).FirstOrDefault();
                    BookingAppointmentVM2 surgery2 = new()
                    {
                        Patient = user,
                        Theater = t == null ? "Not found." : $"{t.Name}",
                        Anae = "DR Fred",
                        Date = app.Date,
                        Codes = codes1,
                        Bed = bed == null ? "Not found." : $"{bed.RoomNumber}",
                        Ward = ward == null ? "Not found." : $"{ward.Name}",
                        Id = app.Id,
                        Time = app.Session

                    };
                    var x11 = new { p = user, a = ConvertQE(ma1), c = ConvertQE(mc1), m = ConvertQE(mm1), hr = new { date, hr }, rr = new { date, rr }, bol = new { date, bol }, bgl = new { date, bgl }, bp = new { date, bps, bpd }, bmi = new { w = admit.Weight, h = admit.Height, bmi = admit.BMI }, booking = surgery2 };
                    return Json(x11);
                }
                BookingAppointmentVM2 surgery = new()
                {
                    Patient = user,
                    Theater = t == null ? "Not found." : $"{t.Name}",
                    Anae = "DR Fred",
                    Date = app.Date,
                    Codes = codes1,
                    Bed = null,
                    Ward = null,
                    Id = app.Id,
                    Time = app.Session

                };
                var x1 = new { p = user, a = ConvertQE(ma1), c = ConvertQE(mc1), m = ConvertQE(mm1), hr = new { date, hr }, rr = new { date, rr }, bol = new { date, bol }, bgl = new { date, bgl }, bp = new { date, bps, bpd }, booking = surgery };
                return Json(x1);

            }


            var x = new { p = user, hr = new { date, hr }, rr = new { date, rr }, bol = new { date, bol }, bgl = new { date, bgl }, bp = new { date, bps, bpd }, a = ConvertQE(ma1), c = ConvertQE(mc1), m = ConvertQE(mm1) };
            return Json(x);
            
        }

        [HttpPost]
        public async Task<JsonResult> CheckPatient([FromBody] MedicineCheck c)
        {
            //NEEDS TO BE TESTED TO THE MAX WITHOUT FAIL
            JSMessage jS;
            var user = _context.PatientUsers.Where(x=>x.IDNumber == c.PatId).First();
            if (string.IsNullOrEmpty(c.PatId) || string.IsNullOrEmpty(c.MedId) || user == null)
            {
                return Json(null);
            }
           
            var activeIn = _context.ActiveIngredients;
            var mAct = _context.MedicineActiveIngredients;
            var mAct1 = mAct.Where(x => x.MedicineId == int.Parse(c.MedId)).AsEnumerable();
            var allergies = _context.Allergies.Where(x => x.PatientId == c.PatId).AsEnumerable();
            foreach (var i in allergies)
            {
                foreach (var item in mAct1)
                {
                    if (item.ActiveIngredientId == i.ActiveIngredientId)
                    {
                        jS = new()
                        {
                            Success = true,
                            Status = false,
                            Message = $"Patient is allergic to {activeIn.Where(x=>x.Id == i.ActiveIngredientId).First().Name}.",
                            Message1 = "Allergy Reaction",
                        };
                        return Json(jS);
                    }
                }
            }
            //check if current chronic condition have a link to selected med
            var patCh = _context.PatientChronicConditions.Where(x => x.PatientID == user.IDNumber).AsEnumerable();
            var condi = _context.ChronicConditions;
            var kk = (from p in patCh
                    join c1 in condi on p.ConditionID equals c1.Id
                    select new
                    {
                        ChronicID = c1.Id,
                        Name = c1.Description,
                    }).AsEnumerable();
            if (kk != null && kk.Any())
            {
                var z = _context.ContraIndications;
                var p = (from k1 in kk
                        join z1 in z on k1.ChronicID equals z1.CondtionID into table1
                        from k2 in table1.AsEnumerable()
                        join m1 in mAct1 on k2.ActiveIngredientId equals m1.ActiveIngredientId
                        select k2).AsEnumerable();
                if (p != null && p.Any())
                {
                    jS = new()
                    {
                        Success = true,
                        Status = false,
                        Message = $"Medicine has a ContraIndication to Patient's Chronic Condition",
                        Message1 = "Medicine ContraIndication",
                    };
                    return Json(jS);
                }
            }


            if (c.Medicine != null && c.Medicine.Count > 0)
            {
                var inter = _context.MedicineInteractions;
                foreach (var it in c.Medicine)
                {
                    var z = await mAct.Where(x => x.MedicineId == it.MedicineId).ToListAsync();
                    if (z.Count() > 0)
                    {
                        foreach (var item in mAct1)
                        {

                            foreach (var be in z)
                            {

                                var k = await inter.Where(b => (b.ActiveIngredient1 == be.ActiveIngredientId || b.ActiveIngredient2 == be.ActiveIngredientId) && (b.ActiveIngredient1 == item.ActiveIngredientId || b.ActiveIngredient2 == item.ActiveIngredientId)).AnyAsync();
                                if (k)
                                {
                                    jS = new()
                                    {
                                        Success = true,
                                        Status = false,
                                        Message = $"{activeIn.Where(x => x.Id == be.ActiveIngredientId).First().Name} has a medical reaction to {activeIn.Where(x => x.Id == item.ActiveIngredientId).First().Name}",
                                        Message1 = "Medicine Interaction",
                                    };
                                    return Json(jS);
                                }
                            }

                        }
                    }
                    
                    
                }
            }
            
            jS = new()
            {
                Success = true,
                Status = true,
                Message = "No issues found.",
                Message1 = "",
            };
            return Json(jS);
        }
        [HttpPost]
        public async Task<JsonResult> BookAppointment([FromBody] BookingAppointmentVM booking)
        {
            
            JSMessage jS = new();
            if (ModelState.IsValid)
            {
                if (DateTime.TryParse($"{booking.Date}", out DateTime dt))
                {
                    if (dt.Date < DateTime.Now.Date)
                    {
                        jS.Success = false;
                        jS.Message = "Incorrect Date";
                        return Json(jS);
                    }
                    SurgeryBooking surgeryBooking = new()
                    {
                        AnaeId = booking.AnaeId,
                        TheaterId = booking.TheaterId,
                        Date = dt,
                        SurgeonId = booking.SurgeonId,
                        PatientIDNumber = booking.PatientIDNumber,
                        Session = booking.Time
                    };
                    await _context.SurgeryBookings.AddAsync(surgeryBooking);
                    await _context.SaveChangesAsync();
                    List<SurgeryBookingTreatmentCode> surgeries = new();
                    foreach (var item in booking.Codes)
                    {
                        SurgeryBookingTreatmentCode x = new()
                        {
                            BookingId = surgeryBooking.Id,
                            TreatmentCode = item.TreatmentId,
                        };
                        surgeries.Add(x);
                    }
                    await _context.SurgeryBookingTreatmentCodes.AddRangeAsync(surgeries);
                    await _context.SaveChangesAsync();
                    //var b = await EmailService.Booking("", "", "", "", "", "");
                    //if (b)
                    //{

                    //}
                    jS = new()
                    {
                        Success = true,
                        Message = "Successfully Booked Appointment"
                    };
                    return Json(jS);
                }

            }
            jS = new()
            {
                Success = false,
                Message = "There is Missing Data"
            };
            return Json(jS);
        }
        public IActionResult Prescription()//
        {
            var Pres = _context.Prescriptions.Where(x=>x.DateTime.Date == DateTime.Now.Date && x.SurgeonId == HttpContext.Session.GetString("id") && x.Status != "Deleted").AsEnumerable();
            var Pat = _context.PatientUsers;
            var m = (from  Pa in Pat
                     join Pr in Pres on Pa.IDNumber equals Pr.PatientId
                     select new PrescriptionList
                     {
                         Priority = Pr.Priority,
                         PresId = Pr.Id,
                         PatId = Pa.IDNumber,
                         Patient = $"{Pa.Name} {Pa.Surname}",
                         Date = Pr.DateTime.ToShortDateString(),
                         Status = Pr.Status,
                     }).Distinct().AsEnumerable();
            return View(m);
        }
        private void LoadDropdownPres()
        {
            var med = _context.Medicines;
            var dosage = _context.Dosages;
            var model = (from m in med
                         join d in dosage on m.DosageId equals d.Id
                         select new
                         {
                             Id = m.Id,
                             Name = $"{m.Name} ({d.Name})"
                         }).Distinct().AsEnumerable().OrderBy(x => x.Name);
            ViewBag.Medicine = new SelectList(model, "Id", "Name");
        }
        private void LoadDropdownAppointment()
        {
            ViewBag.Anae = new SelectList(_context.ApplicationUsers.Where(x => x.Role == Roles.Anaesthesiologist), "Id", "Name");
            ViewBag.Theaters = new SelectList(_context.Theaters, "Id", "Name");
            ViewBag.TreatmentCodes = new SelectList(_context.TreatmentCodes, "TreatmentId", "TreatmentName");


        }
        public async Task<IActionResult> AddPrescription(string? id, int? PresID)
        {
            LoadDropdownPres();
            if (id != null && PresID != null)
            {
                var x = await _context.Prescriptions.Where(x => x.PatientId == id && x.Id == PresID).FirstOrDefaultAsync();
                if (x != null)
                {
                    x.IsNew = true;
                    _context.Prescriptions.Update(x);
                    await _context.SaveChangesAsync();
                }
                return View(new { Id = id.ToString() });
            }
            if (id != null)
            {
                return View(new { Id = id });
            }
            return View(new { Id = "" });
        } 
        [HttpGet]
        public async Task<JsonResult> SearchPatientPrescription(string Id)// Get list of prescriptions for a patient
        {
            var pat = _context.PatientUsers.Where(x => x.IDNumber == Id).FirstOrDefault();
            if (pat == null)
            {
                return Json(null);
            }
            var mod = _context.Prescriptions.Where(x=>x.PatientId == pat.IDNumber && x.Status != "Deleted").Select(x=> new PrescriptionVM1
            {
                PatientIDNumber = pat.IDNumber,
                PatientName = $"{pat.Name} {pat.Surname}",
                Date = x.DateTime.ToShortDateString(),
                Status = x.Status,
                PrescriptionId = x.Id,
                Priority = x.Priority,
            }).AsEnumerable();
            
            return Json(mod);
        }
        
        [HttpGet]
        public async Task<JsonResult> SearchPatientBooking(string Id) // get bookings of a specific date
        {
            if (DateTime.TryParse(Id, out DateTime dt))
            {
                var Pat = _context.PatientUsers;
                var Theater = _context.Theaters;
                var booking = _context.SurgeryBookings.Where(x => x.Date.Date == dt.Date.Date && x.SurgeonId == HttpContext.Session.GetString("id")).AsEnumerable();
                var m = (from P in Pat
                         join B in booking on P.IDNumber equals B.PatientIDNumber into table1
                         from B in table1.ToList()
                         join T in Theater on B.TheaterId equals T.Id
                         select new BookingList
                         {
                             PatId = P.IDNumber,
                             Patient = $"{P.Name} {P.Surname}",
                             Theater = T.Name,
                             Date = B.Date.ToShortDateString(),
                             Time = B.Session,
                             IsConfirmed = B.IsConfirmed,
                             BookingId = B.Id
                         }).Distinct().AsEnumerable();
                return Json(m);
            }
            return Json(null);// prescribed 
        }
        [HttpPost]
        public JsonResult AddPrescription1([FromBody] PrescriptionVM prescription)
        {
            JSMessage jS = new();
            if (ModelState.IsValid)
            {
                if (DateTime.TryParse(prescription.Date, out DateTime dt))
                {
                    
                    Prescription p = new()
                    {
                        SurgeonId = prescription.SurgeonId,
                        PatientId = prescription.PatientId,
                        DateTime = dt,
                        Priority = prescription.Priority,
                        RejectReason = "",
                        IsNew = false,
                    };
                    _context.Prescriptions.Add(p);
                    _context.SaveChanges();

                    prescription.Medicine.ForEach(m => m.PrescriptionId = p.Id);
                    prescription.Medicine.ForEach(m => m.Id = 0);
                    var z = prescription.Medicine;
                    _context.PrescriptionMedicines.AddRange(prescription.Medicine);
                    _context.SaveChanges();

                    jS.Success = true;
                    jS.Status = true;
                    jS.Message = "Successfully added prescription";
                    return Json(jS);
                }
            }
            return Json(null);
        }
        static PrescriptionVM Presc;
        
        public IActionResult Patient()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmBooking(int? Id)
        {
            if (Id == null)
            {
                TempData["error"] = "Sorry, Record was not found.";
                return RedirectToAction(nameof(Index));
            }
            var x = await _context.SurgeryBookings.Where(x => x.Id == Id && x.SurgeonId == HttpContext.Session.GetString("id")).FirstAsync();
            if(x == null)
            {
                TempData["error"] = "Sorry, Record was not found.";
                return RedirectToAction(nameof(Index));
            }
            x.IsConfirmed = true;
            _context.SurgeryBookings.Update(x);
            await _context.SaveChangesAsync();
            TempData["pass"] = "Booking has been confirmed";
            return RedirectToAction(nameof(Index));
        }
       
        [HttpGet]
        public JsonResult PatientHistory(string ID)
        {
            if (ID == "1234567891234")
            {
                List<string> s = ["Peniciline", "Panado", "Asprine"];
                List<string> c = ["Diabetic", "Cancer", "Lorerm"];
                List<string> m = ["Morphine (Pill)", "Iron Supplements (Pill)", "Aspirin"];
                PatientUser user = new()
                {
                    IDNumber = ID,
                    Email = "dsad@gmai.com",
                    PhoneNumber = "1234567890",
                    Gender = "Male",
                    DateOfBirth = DateTime.Now,
                    AddressLine1 = "street",
                    AddressLine2 = "PE",
                    Name = "Simon B"
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
        public async Task<IActionResult> ViewAppointment(string? Id)
        {
            if (Id == null)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Appointments));
            }
            var booking = await _context.SurgeryBookings.Where(x => x.Id == int.Parse(Id) && x.SurgeonId == HttpContext.Session.GetString("id")).FirstAsync();
            if (booking == null)
            {
                TempData["error"] = "Sorry. Could not find record";
                return RedirectToAction(nameof(Appointments));
            }
            var bCodes = _context.TreatmentCodes;
            var pbcodes = _context.SurgeryBookingTreatmentCodes.Where(x => x.BookingId == booking.Id).AsEnumerable();
            var codes = (from co in pbcodes
                         join bo in bCodes on co.TreatmentCode equals bo.TreatmentId
                         select bo).AsEnumerable();
            var user = await _context.PatientUsers.Where(x => x.IDNumber == booking.PatientIDNumber).FirstAsync();
            var theater = _context.Theaters.Where(x => x.Id == booking.TheaterId).First().Name;
            //string? Anae = _context.ApplicationUsers.Where(x => x.IDNumber == booking.AnaeId).First().Name; COME BACK AND FIX HERE
            var admit = await _context.AdmittedPatients.Where(x => x.BookingID == booking.Id).FirstOrDefaultAsync();
            BookingAppointmentVM2 surgery;
            if (admit != null)
            {
                var bed = await _context.Beds.Where(x => x.Id == admit.BedID).FirstOrDefaultAsync();
                var ward = await _context.Wards.Where(x => x.Id == bed.WardId).FirstOrDefaultAsync();

                surgery = new()
                {
                    Patient = user,
                    Theater = theater,
                    Anae = null,
                    Date = booking.Date.Date,
                    Codes = codes,
                    Bed = bed.RoomNumber,
                    Ward = ward.Name,
                    Id = booking.Id,
                    Time = booking.Session,
                    IsConfirmed = booking.IsConfirmed,
                    Status = booking.Status

                };
            }
            else
            {
                surgery = new()
                {
                    Patient = user,
                    Theater = theater,
                    Anae = null,
                    Date = booking.Date.Date,
                    Codes = codes,
                    Bed = null,
                    Ward = null,
                    Id = booking.Id,
                    Time = booking.Session,
                    IsConfirmed = booking.IsConfirmed,
                    Status = booking.Status
                };
            }
            
            return View(surgery);
        }
    }
}
