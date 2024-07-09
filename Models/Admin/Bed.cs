using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class Bed
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Ward")]
        public int WardId { get; set; }
        [Display(Name = "Room Number")]

        public string RoomNumber { get; set; }
        public string Status { get; set; } = "Available";
    }
}
