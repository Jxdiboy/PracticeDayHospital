using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SurgeonId { get; set; }
        [Required]
        public string PatientId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Status { get; set; } = "Prescribed";
        [Required]
        public string Priority { get; set; }
        public string? PharamID { get; set; }
        public string? RejectReason { get; set; }
        public bool IsNew { get; set; } = false;
        public List<PrescriptionMedicine> Medicines { get; set; }

    }
}
