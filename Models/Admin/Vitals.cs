using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class Vitals
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public double Maximum { get; set; }
        
        public double Minimum { get; set; }

    }
}
