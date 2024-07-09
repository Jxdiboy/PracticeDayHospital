using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class TreatmentCode
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Treatment Code")]

        public string TreatmentId { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Description")]

        public string TreatmentName { get; set; }
    }
}
