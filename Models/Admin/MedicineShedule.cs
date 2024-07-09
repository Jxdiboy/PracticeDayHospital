using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class MedicineSchedule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
