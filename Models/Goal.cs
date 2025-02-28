using System;
using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class Goal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име на целта.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Моля, въведете начални килограми.")]
        [Range(30, 200, ErrorMessage = "Килограмите трябва да са между 30 и 200.")]
        public double StartWeight { get; set; }

        [Required(ErrorMessage = "Моля, въведете целеви килограми.")]
        [Range(30, 200, ErrorMessage = "Килограмите трябва да са между 30 и 200.")]
        public double TargetWeight { get; set; }

        [Required(ErrorMessage = "Моля, въведете начална дата.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Моля, въведете крайна дата.")]
        public DateTime EndDate { get; set; }
    }
}
