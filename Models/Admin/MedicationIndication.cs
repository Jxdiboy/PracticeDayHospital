using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class MedicationIndication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MedicationId { get; set; }
        public int IndicationId { get; set; }
    }

}
