using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class Theater
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Room Number")]
        public int RoomNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
