using CarServiceMVCApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarServiceMVCApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<RepairJob> RepairJobs { get; set; }
    }
}
