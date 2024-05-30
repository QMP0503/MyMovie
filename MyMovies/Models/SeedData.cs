using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyMovies.Data;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MyMovies.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MovieDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MovieDbContext>>()))
        {
            var JsonImport = new JsonImport();

            // Look for any movies.
            if (context.Movies.Any())
            {
                return;   // DB has been seeded
            }
            JsonImport.Import(context);

            if (context.Actors.Any())
            {
                return;   // DB has been seeded
            }


            context.SaveChanges();
        }
    }
}