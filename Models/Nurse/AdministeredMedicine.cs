using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Nurse
{
    public class AdministeredMedicine
    {
        [Key]
        public int Id { get; set; }
        public int PrescriptionID {  get; set; }
        public int AdmittedPatientID {  get; set; }

        public DateTime? Time {  get; set; }

    }
}
