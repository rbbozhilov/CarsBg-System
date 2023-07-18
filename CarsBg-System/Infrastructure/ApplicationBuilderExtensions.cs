using CarsBg_System.Data;
using CarsBg_System.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarsBg_System.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {

        //Method create on each start project new migration and put some data in database
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopeServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopeServices.ServiceProvider;
            var data = serviceProvider.GetService<CarsDbContext>();

            data.Database.Migrate();

            SeedCategories(data);
            SeedExtras(data);
            SeedBrands(data);
            SeedEngines(data);
            SeedRegions(data);
            SeedTransmissions(data);
            SeedWheelDrives(data);
            SeedAdminRole(serviceProvider);
            SeedModeratorRole(serviceProvider);

            return app;
        }

        private static void SeedBrands(CarsDbContext data)
        {
            if (data.Brands.Any())
            {
                return;
            }

            data.Brands.AddRange
                (
                new[]
                {
                    new Brand() { Name = "BMW"},
                    new Brand() { Name = "Mercedes-Benz"},
                    new Brand() { Name = "Audi"},
                    new Brand() { Name = "Golf"}
                });

            data.SaveChanges();
        }

        private static void SeedCategories(CarsDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange
                (
                new[]
                {
                    new Category() { Name = "Sedan"},
                    new Category() { Name = "Kombi"},
                    new Category() { Name = "HechBek"}
                });

            data.SaveChanges();
        }

        private static void SeedEngines(CarsDbContext data)
        {
            if (data.Engines.Any())
            {
                return;
            }

            data.Engines.AddRange
                (
                new[]
                {
                    new Engine() { Name = "Diesel"},
                    new Engine() { Name = "Gasoline"},
                    new Engine() { Name = "Hybrid"},
                    new Engine() { Name = "Electric"}
                });

            data.SaveChanges();
        }

        private static void SeedExtras(CarsDbContext data)
        {
            if (data.Extras.Any())
            {
                return;
            }

            data.Extras.AddRange
                (
                new[]
                {
                    new Extra() { Name = "Comfort"},
                    new Extra() { Name = "Bi-xenon"},
                    new Extra() { Name = "Leather"},
                    new Extra() { Name = "Parktroning"}
                });

            data.SaveChanges();
        }

        private static void SeedRegions(CarsDbContext data)
        {
            if (data.Regions.Any())
            {
                return;
            }

            data.Regions.AddRange
                (
                new[]
                {
                    new Region() { Name = "Sofia"},
                    new Region() { Name = "Plovdiv"},
                    new Region() { Name = "Varna"},
                    new Region() { Name = "Burgas"},
                    new Region() { Name = "Ruse"}
                });

            data.SaveChanges();
        }

        private static void SeedTransmissions(CarsDbContext data)
        {
            if (data.Transmissions.Any())
            {
                return;
            }

            data.Transmissions.AddRange
                (
                new[]
                {
                    new Transmission() { Name = "Manual"},
                    new Transmission() { Name = "Automatic"}
                });

            data.SaveChanges();
        }

        private static void SeedWheelDrives(CarsDbContext data)
        {
            if (data.WheelDrives.Any())
            {
                return;
            }

            data.WheelDrives.AddRange
                (
                new[]
                {
                    new WheelDrive() { Name = "Rear"},
                    new WheelDrive() { Name = "Front"},
                    new WheelDrive() { Name = "4x4"}
                });

            data.SaveChanges();
        }

        private static void SeedAdminRole(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Administrator"))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Administrator" };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@carsbg.net";
                    const string adminPassword = "admin12";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }


        private static void SeedModeratorRole(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Moderator"))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Moderator" };

                    await roleManager.CreateAsync(role);

                    const string moderatorEmail = "moderator@carsbg.net";
                    const string moderatorPassword = "moderator12";

                    var user = new User
                    {
                        Email = moderatorEmail,
                        UserName = moderatorEmail
                    };

                    await userManager.CreateAsync(user, moderatorPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }


    }
}
