using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace MyMovies.Data
{
    public class ApplicationDbContext:IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {  }

        public DbSet<Movie250> Movies250 { get; set;}
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the primary key for IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(login => new { login.LoginProvider, login.ProviderKey });

            modelBuilder.Entity<Movie>().ToTable(nameof(Movies))
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity<MovieActor>();

            modelBuilder.Entity<Movie>().ToTable(nameof(Movies))
                .HasMany(d => d.Directors)
                .WithMany(m => m.Movies)
                .UsingEntity<MovieDirector>(); //configure model class to act as link (not needed but helps with navigation)

            //.UsingEntity("MovieDirector",
            //        l => l.HasOne(typeof(Director)).WithMany().HasForeignKey("MoviesId").HasPrincipalKey(nameof(Director.Id)),
            //        r => r.HasOne(typeof(Movie)).WithMany().HasForeignKey("DirectorsId").HasPrincipalKey(nameof(Movie.Id)),
            //        j => j.HasKey("MoviesId", "DirectorsId"));
            // full config for learning purposes
        }
    }
}
