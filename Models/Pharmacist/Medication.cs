using System.ComponentModel.DataAnnotations;
namespace DayHospital.Models.Pharmacist
{
    public class Medication
    {
        [Key]
        public int medicationId { get; set; }
        [Required]
        public int medicationScheduleId { get; set; }
        [Required]
        public string medicationName { get; set;}
        [Required]
        public int dosageID { get; set; }
        [Required]
        public int quantity { get;}
        [Required]
        public int reOrderLevel { get; set;}
    }
}
