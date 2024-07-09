using System.ComponentModel.DataAnnotations;
namespace DayHospital.Models.Nurse
{
    public class PatientAllergies
    {
        [Key]
        public int Id { get; set; }
        
        public string PatientId { get; set;}
        
        public int ActiveIngredientId { get; set; }
        
    }
}
