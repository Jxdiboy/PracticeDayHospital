using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class ChronicCondition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
