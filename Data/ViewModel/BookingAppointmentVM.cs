using DayHospital.Models;
using System.ComponentModel.DataAnnotations;

namespace DayHospital.Data.ViewModel
{
    public class BookingAppointmentVM // from the form
    {
        
        public List<TreatmentCode> Codes { get; set;}
        [Required]
        public string SurgeonId { get; set; }
        public string? AnaeId { get; set; }
        
        public string PatientIDNumber { get; set; }
        
        public int TheaterId { get; set; }
        
        public string Date { get; set; }
        
        public string Time { get; set; }
        public int? Id { get; set; }
        
    }
    public class BookingAppointmentVM2 // View Appoinemt
    {
        
        public IEnumerable<TreatmentCode> Codes { get; set;}
        
        public string SurgeonId { get; set; }
        public bool IsConfirmed { get; set; }
        public string? Anae { get; set; } = string.Empty;
        
        public PatientUser Patient { get; set; }
        
        public string Theater { get; set; }
        
        public DateTime Date { get; set; }
        public string Time { get; set; } // please mind
        public string? Bed { get; set; }
        public string? Ward { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }
    }
    public class BookingAppointmentVM1
    {
        public string PatientIDNumber { get; set; }
        public string Date { get; set; }
        public string Theater { get; set; }
        public string Time { get; set; }
        public int BookingId { get; set; }
    }
}
