using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class PatientUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]

        public string Name { get; set; }
        [Required]
        [StringLength(50)]

        public string Surname { get; set; }
        [Required]
        [StringLength(13)]

        public string IDNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(10)]

        public string PhoneNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(50)]

        public string? AddressLine1 { get; set; }
        [StringLength(50)]

        public string? AddressLine2 { get; set; }
        public int? SuburbId { get; set; }// we need to remove address line 2 because of the suburb id 
    }
}
