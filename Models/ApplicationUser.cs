using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Specialization {  get; set; }
        [Required]
        public string Role {  get; set; }
        [Required]
        public int RegistrationNumber { get; set; }
        [StringLength(50)]
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public int CityId { get; set;}
        [Required]
        [StringLength(13)]

        public string IDNumber { get; set; }
    }
}
