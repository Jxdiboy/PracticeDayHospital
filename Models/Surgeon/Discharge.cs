using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class Discharge
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AdmittedId { get; set; }
        [Required]
        public string SurgeonNote { get; set; }
        public string? NurseNote { get; set; }
        public DateTime? NurseDateTime { get; set; }
        public DateTime SurgeonDateTime { get; set; }

    }
}
