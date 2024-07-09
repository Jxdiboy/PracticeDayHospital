using System.ComponentModel.DataAnnotations;
namespace DayHospital.Models.Pharmacist
{
    public class OrderMed
    {
        [Key]
        public int orderId { get; set; }
        [Required]
        public int pharmacistID { get; set; }
        [Required]
        public DateTime date { get; set; }
    }
}
