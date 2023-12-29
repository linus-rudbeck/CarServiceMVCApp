using System.ComponentModel.DataAnnotations;

namespace CarServiceMVCApp.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required, Range(1800, int.MaxValue)]
        public int Year { get; set; }

        [Required, MinLength(1)]
        public string Make { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }


        public string? Notes { get; set; }

        public List<RepairJob> RepairJobs { get; set; }
    }
}
