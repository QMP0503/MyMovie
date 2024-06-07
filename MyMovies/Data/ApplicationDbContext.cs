using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMovies.ViewModels;
namespace MyMovies.Data
{
    public class ApplicationDbContext:IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {  }

        public DbSet<Movie250> Movies250 { get; set;}

        //connection (many to many) using one to many below
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }

        //connection (one to many)
        public DbSet<MovieActor> MovieActors { get; set; } 
        public DbSet<MovieDirector> MovieDirectors { get; set; } 
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>().HasIndex(e => e.Name).IsUnique();

            modelBuilder.Entity<Director>().HasIndex(e => e.Name).IsUnique();

            // Configure the primary key for IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(login => new { login.LoginProvider, login.ProviderKey });

            modelBuilder.Entity<Movie>() //movie actor
                .HasMany(m => m.MovieActors)
                .WithOne(ma => ma.Movie)
                .HasForeignKey(ma => ma.MovieId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            

            modelBuilder.Entity<Movie>() //movie director
                .HasMany(m => m.MovieDirectors)
                .WithOne(md => md.Movie)
                .HasForeignKey(md => md.MovieId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Director>()
                .HasMany(d => d.MovieDirectors)
                .WithOne(m => m.Director)
                .HasForeignKey(m => m.DirectorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Actor>()
                .HasMany(a => a.MovieActors)
                .WithOne(m => m.Actor)
                .HasForeignKey(m => m.ActorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
              

        }
    
    }
}
