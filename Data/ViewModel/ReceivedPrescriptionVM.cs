using DayHospital.Models;
namespace DayHospital.Data.ViewModel
{
    public class ReceivedPrescriptionVM
    {
        public int PrescriptionId { get; set; }
        public string PatientName { get; set; }
        public string Ward { get; set; }
        public string Bed { get; set; }
        public List<MedicationVM> Medications { get; set; }
    }
}
