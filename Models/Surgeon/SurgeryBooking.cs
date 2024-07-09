using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class SurgeryBooking
    {
        [Key]
        public int Id { get; set; }
        [Required]  
        public string SurgeonId { get; set; }
        public string? AnaeId { get; set; }
        [StringLength(13)]
        [Required]
        public string PatientIDNumber { get; set; }
        [Required]
        public int TheaterId { get; set;}
        [Required]
        public DateTime Date { get; set;}
        public string Session { get; set;}
        public bool IsDone { get; set; } = false; // after the surgeon sends the note it will be true
        public bool IsConfirmed { get; set;}
        public string Status { get; set; } = "Booked";
    }
}
