using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]

        public string Name { get; set; }
    }
}
