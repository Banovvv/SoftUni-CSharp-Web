using System.ComponentModel.DataAnnotations;

namespace WebServer.MvcFramework.Users
{
    public class UserIdentity<T>
    {
        public T Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
