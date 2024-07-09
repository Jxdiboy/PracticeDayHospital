using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class Indication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
    public class ContraIndication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ActiveIngredientId { get; set; }
        [Required]
        public int CondtionID { get; set; }
    }

}
