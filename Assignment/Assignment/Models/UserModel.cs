using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
