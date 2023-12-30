using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarServiceMVCApp.Models
{
    public class RepairJob
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public string RepairName { get; set; }


        public string? Description { get; set; }
        public bool Completed { get; set; } =false;

        public string? TechnicianName { get; set; }

        [ValidateNever]
        public Car Car { get; set; }
    }
}
