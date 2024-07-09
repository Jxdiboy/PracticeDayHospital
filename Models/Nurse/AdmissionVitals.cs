using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Nurse
{
    public class AdmissionVitals
    {
        [Key]
        public int Id { get; set; }
        public int VitalsId { get; set; }
        public int admissionID { get; set; }
        public DateTime? Time { get; set; }
        public int Reading1 { get; set; }
        public int? Reading2 { get; set; }
        public int? Reading3 { get; set; }
          

    }
}
