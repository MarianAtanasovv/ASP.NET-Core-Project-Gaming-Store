using GameStore.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Infrastructure
{

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedGenres(services);
            SeedPlatforms(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();
        }

        private static void SeedGenres(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
            {
                new Genre {Name = "Action"},
                new Genre {Name = "Shooter"},
                new Genre {Name = "Fighting"},
                new Genre {Name = "Stealth"},
                new Genre {Name = "Survival"},
                new Genre {Name = "Action-Adventure"},
                new Genre {Name = "Survival horror"},
                new Genre {Name = "Role-Playing"},
                new Genre {Name = "Simulation"},
                new Genre {Name = "Strategy "},
                new Genre {Name = "RTS"},
                new Genre {Name = "Tower Defense"},

            });

            data.SaveChanges();
        }

        private static void SeedPlatforms(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.Platforms.Any())
            {
                return;
            }

            data.Platforms.AddRange(new[]
            {
                new Platform {Name = "Playstation"},
                new Platform {Name = "PC"},
                new Platform {Name = "Nintendo"},
                new Platform {Name = "Xbox 360"},

            });

            data.SaveChanges();
        }
        private static void SeedAdministrator(IServiceProvider services)
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

                    const string adminUsername = "marian345";
                    const string adminEmail = "test123@abv.bg";
                    const string adminPassword = "test123";
                    

                    var user = new User
                    {
                        UserName = adminUsername,
                        Email = adminEmail,
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }


    }
}
