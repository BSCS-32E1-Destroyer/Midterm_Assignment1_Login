using System.ComponentModel.DataAnnotations;

namespace Midterm_Assignment1_Login.Models.Entities
{
    public class CookieUser
    {
        [Key]
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}
