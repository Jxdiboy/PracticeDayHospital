using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class Ward
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Ward Name")]
        public string Name { get; set; }
    }

}
