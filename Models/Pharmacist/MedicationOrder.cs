using System.ComponentModel.DataAnnotations;
namespace DayHospital.Models.Pharmacist
{
    public class MedicationOrder
    {
        [Key]
        public int medicationId { get; set; }
        [Required]
        public int orderId { get; set; }
        [Required]
        public int medicineID { get; set; }
        [Required]
        public string quantity { get; set; }
    }
}
