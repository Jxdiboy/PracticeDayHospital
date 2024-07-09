using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class DayHospitalRecords
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Hospital Name")]
        public string Name { get; set; }

        [StringLength(50)]
        public string? AddressLine1 { get; set; }
        public int? SuburbId { get; set; }

        [Required]
        [StringLength(10)]
        public string ContactNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Purchase Manager E-Mail")]
        public string PMEmail { get; set; }

    }
}
