using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
namespace DayHospital.Models.Nurse
{
    public class AdmittedPatient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NurseId { get; set; }
        [Required] 
        public string PatientID { get; set; }
        [Required]
        public int Height {  get; set; }
        [Required]
        public int BedID {  get; set; }
        public int BookingID {  get; set; }
        public double Weight {  get; set; }
        public double BMI {  get; set; }
        [Required]
        public string TreatmentCode {  get; set; }
        [Required]
        public DateTime DateTime { get; set; }

    }
}
