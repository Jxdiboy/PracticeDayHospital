using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class SurgeryBookingTreatmentCode
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TreatmentCode { get; set; }
        [Required]
        public int BookingId { get; set; }
    }
}
