﻿using System.ComponentModel.DataAnnotations;

namespace DayHospital.Models.Admin
{
    public class ActiveIngredient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}