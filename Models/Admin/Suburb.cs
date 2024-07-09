using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class Suburb
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CityID { get; set; }
        [Required]
        [StringLength(50)]

        public string Name { get; set; }
    }
}
