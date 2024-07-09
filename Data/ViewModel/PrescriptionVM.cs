using DayHospital.Models;
using System.ComponentModel.DataAnnotations;

namespace DayHospital.Data.ViewModel
{
    public class PrescriptionVM
    {
        [Required]
        public string SurgeonId { get; set; }
        public string PatientId { get; set; }
        public string Date{ get; set; }
        public string? Reason { get; set; }
        public string Priority { get; set; }
        public PatientUser? User { get; set; }
        public List<PrescriptionMedicine> Medicine { get; set; }
        public string Status {  get; set; }
        public string? Pharama {  get; set; }
    }
    public class PrescriptionVM1
    {
        public string PatientIDNumber { get; set; }
        public string PatientName { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public int PrescriptionId { get; set; }
    }
}
