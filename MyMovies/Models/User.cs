using Microsoft.AspNetCore.Identity;
namespace MyMovies.Models
{
    public class User:IdentityUser
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
