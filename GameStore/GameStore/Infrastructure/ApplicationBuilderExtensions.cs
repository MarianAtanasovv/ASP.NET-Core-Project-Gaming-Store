using GamingWebAppDb;
using GamingWebAppDb.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            SeedGenres(data);

            return app;
        }

        private static void SeedGenres(ApplicationDbContext data)
        {
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
    }
}
