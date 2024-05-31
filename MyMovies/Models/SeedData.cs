using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyMovies.Data;
using System;
using System.Linq;
using System.Text.Json;

namespace MyMovies.Models;

public static class SeedData
{
        
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
        {
            // Look for any movies.
            if (context.Movies.Any())
            {
                return;   // DB has been seeded
            }
            context.Movies.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Rating = 8,
                    
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Rating = 6
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Rating = 2
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Rating = 10
                }
            );
            if (context.Actors.Any())
            {
                return;   // DB has been seeded
            }
            context.Actors.AddRange(
                new Actor
                {
                    FirstName = "Jason",
                    LastName = "Cruise",
                    Age = 30,
                },
                new Actor
                {
                    FirstName = "Tom",
                    LastName = "Cruise",
                    Age = 40,
                     
                },
                new Actor
                {
                    FirstName = "Jack",
                    LastName = "Born",
                    Age = 20,
                },
                new Actor
                {
                    FirstName = "Tom",
                    LastName = "Smith",
                    Age = 50,

                },
                new Actor
                {
                    FirstName = "James",
                    LastName = "Ryan",
                    Age = 30,
                },
                new Actor
                {
                    FirstName = "Sarah",
                    LastName = "Bonny",
                    Age = 22,
                }
                );


 
           // List<MovieTest> movieTest = JsonSerializer.Deserialize<List<MovieTest>>(File.ReadAllText("movies-250-edit.json"));

            //context.MovieTests.AddRange(movieTest);
            context.SaveChanges();
        }
    }
}