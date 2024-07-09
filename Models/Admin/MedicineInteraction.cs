using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class MedicineInteraction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ActiveIngredient1 { get; set; }
        public int ActiveIngredient2 { get; set; }
    }
}
