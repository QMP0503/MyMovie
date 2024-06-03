using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace MyMovies.Data
{
    public class ApplicationDbContext:IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {  }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie250> Movies250 { get; set;}
    }
}
