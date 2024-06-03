using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyMovies.Data;
using MyMovies.Migrations;
using MyMovies.Models.ReadJsonModels;
using Newtonsoft.Json;
using NuGet.Versioning;
using System;
using System.Globalization;
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

            //StreamReader r = new StreamReader("C:\\Users\\intern.pmquang1\\C#\\MyMovies\\MyMovies\\wwwroot\\movies-250.json");

            //string json = r.ReadToEnd();
            //MovieTestArray movieTest = JsonConvert.DeserializeObject<MovieTestArray>(json);
            //var movieArray = new List<Movie250>();
            //int i = 0;
            //IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
            //foreach (var movie in movieTest.movies)
            //{
            //    int.TryParse(movie.BoxOffice, NumberStyles.Integer | NumberStyles.AllowThousands,
            //            provider, out int boxOffice);
            //    int.TryParse(movie.Year, out int year);
            //    int.TryParse(movie.imdbVotes, NumberStyles.Integer | NumberStyles.AllowThousands,
            //            provider, out int votes);
            //    int.TryParse(movie.Metascore, NumberStyles.Integer | NumberStyles.AllowThousands, provider, out int metascore);
            //    double.TryParse(movie.imdbRating, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
            //            provider, out double rating);
            //    DateOnly.TryParse(movie.Released, CultureInfo.InvariantCulture,  out DateOnly released);
            //    movieArray.Add(
            //    new Movie250
            //    {
            //        Title = movie.Title,
            //        Year = year,
            //        Actors = movie.Actors,
            //        Rated = movie.Rated,
            //        Release = released,
            //        Runtime = movie.Runtime,
            //        Genre = movie.Genre,
            //        Director = movie.Director,
            //        Writer = movie.Writer,
            //        Plot = movie.Plot,
            //        Language = movie.Language,
            //        Country = movie.Country,
            //        Awards = movie.Awards,
            //        Poster = movie.Poster,
            //        Metascore = metascore,
            //        imdbRating = rating,
            //        imdbVotes = votes,
            //        BoxOffice = boxOffice,
            //    });
            //}
            ////var test = movieArray.OrderByDescending(x => x.Release);
            //if (context.Movies250.Any())
            //{
            //    return;   // DB has been seeded
            //}
            //context.Movies250.AddRange(movieArray);

            //var directorList =  new List<DirectorTemp>();

            //var movie250 = context.Movies250.ToList();

            //int i = 0;

            //foreach (var movie in movie250)
            //{
            //    string[] directorArray = movie.Director.Split(", ");
            //    foreach(var director in directorArray)
            //    {
            //        if(directorList.Any(x => x.Name == director))
            //        {
            //            continue;
            //        }
            //        directorList.Add(new DirectorTemp
            //        {
            //            Id = ++ i,
            //            Name = director
            //        });
            //    }     
            //}
            //var test = directorList.Count();

            //var distinct = directorList.Select(x => x).Distinct();

            //if (context.DirectorTemp.Any())
            //{
            //    return;   // DB has been seeded

            //}

            //context.DirectorTemp.AddRange(directorList);

            var directors = context.DirectorTemp.ToList();  
            var movie = context.Movies250.ToList();

            var directorToDb = new List<Director>();
            var movieDb = new List<Movie>();


            context.Movies.AddRange();


            //foreach(var mov in movie)
            //{
            //    string[] directorArray = mov.Director.Split(", ");
                
            //    foreach(var director in directorArray)
            //    {
            //       directorToDb.Add(new Director
            //        {
            //            Name = director,
            //        });
            //    }
            //}


            context.SaveChanges();


        }
    }
}