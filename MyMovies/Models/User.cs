using Microsoft.AspNetCore.Identity;
namespace MyMovies.Models
{
    public class User:IdentityUser
    {
        [MaxLength(100)]
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
