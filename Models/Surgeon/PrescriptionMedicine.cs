using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class PrescriptionMedicine
    {
        [Key]
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public int? AdministeredMedicineId { get; set; }
        [Required]
        public string Instructions { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int AdministeredQuantity { get; set; } = 0;
        public DateTime AdministeredDateTime { get; set; } = DateTime.Now;
        [Required]
        public int MedicineId { get; set; }
        public string? Name { get; set; }
        public string? Reason { get; set; }
        public string? Reason1 { get; set; }
        public string? PharmacistReason { get; set; }
        public string? PharmacistId { get; set; }
    }
    
}
