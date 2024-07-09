using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]

        public int ProvinceID { get; set; }
    }
}
