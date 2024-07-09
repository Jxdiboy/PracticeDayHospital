using System.ComponentModel.DataAnnotations;
namespace DayHospital.Models.Pharmacist
{
    public class MedicatonSchedule
    {
        [Key]
        public int medicatonScheduleId { get; set; }
        [Required]
        public int level { get; set; }
    }
}
