using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace MyMovies.Data
{
    public class ApplicationDbContext:IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {  }

        public DbSet<Movie> Movies { get; set; }
       // public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie250> Movies250 { get; set;}
        public DbSet<DirectorTemp> DirectorTemp { get; set; }
    //    public DbSet<Director> Directors { get; set; }
    }
}
