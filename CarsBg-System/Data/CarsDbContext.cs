using CarsBg_System.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarsBg_System.Data
{
    public class CarsDbContext : IdentityDbContext<User>
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Engine> Engines { get; set; }

        public virtual DbSet<Model> Models { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<Transmission> Transmissions { get; set; }

        public virtual DbSet<Extra> Extras { get; set; }

        public virtual DbSet<WheelDrive> WheelDrives { get; set; }



    }
}