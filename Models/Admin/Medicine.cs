using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]

        public string Name { get; set; }
        [Required]
        [StringLength(50)]

        public string Description { get; set; }
        public int DosageId { get; set; }
        public int ScheduleLevelId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ReOrderLevel { get; set; }
    }
}
