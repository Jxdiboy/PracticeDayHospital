using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class MedicineActiveIngredient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MedicineId { get; set; }
        public int ActiveIngredientId { get; set; }
        [Required]
        public int Strength { get; set; }
    }
}
