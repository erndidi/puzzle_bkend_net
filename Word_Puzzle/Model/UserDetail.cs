using System.ComponentModel.DataAnnotations.Schema;

namespace Puzzle_API.Model
{
    [Table("UserDetails")]
    public class UserDetail
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string? FirstName { get; set; }

        public string? Lastname { get; set; }


        public string? GoogleId { get; set; }

        public string? FacebookId { get; set; }

        public int Score { get; set; }

        public string? UserWords { get; set; }

        public string? SessionId { get; set; }

        public int Attempts { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public string UserName { get; set; }         

        public string Password { get; set; }


        public UserSession UserSession { get; set; } // Navigation property


    }
}
