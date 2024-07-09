using DayHospital.Models;
namespace DayHospital.Data.ViewModel
{
    public class PrescriptionDetailsVM
    {
        public class PrescriptionDetailsViewModel
        {
            public int PrescriptionId { get; set; }
            public string PatientName { get; set; }
            public string PatientID { get; set; }
            public string PharmacistName { get; set; }
            public DateTime PrescriptionDate { get; set; }
            public List<MedicationVM> Medications { get; set; }
        }
    }
}
