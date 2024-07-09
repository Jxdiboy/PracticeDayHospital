using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class ExtraReading
    {
        [Key]
        public int Id { get; set; }
        public string Vitals { get; set; }
        public string SurgeonId { get; set; }
        public string PatientId { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public bool? IsDone { get; set; } = false;
    }
}
