using DayHospital.Models;

namespace DayHospital.Data.ViewModel
{
    public class MedicineCheck
    {
        public string? PatId { get; set; }
        public string? MedId { get; set; }
        public List<PrescriptionMedicine>? Medicine { get; set; }
    }
}
