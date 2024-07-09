using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Nurse
{
    public class PatientCurrentMedication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PatientID { get; set; }
        public int MedicineID { get; set; }
    }
}
