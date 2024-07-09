using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Nurse
{
    public class PatientChronicCondition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NurseID { get; set; }
        [Required]
        public string PatientID { get; set; }
        [Required]
        public int ConditionID {  get; set; }

    }
}
