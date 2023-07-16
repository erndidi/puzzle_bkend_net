using System.ComponentModel.DataAnnotations.Schema;

namespace Puzzle_API.Model
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public string SessionId { get; set; }

        [ForeignKey("UserId")]
        public UserDetail UserDetail { get; set; }
        public Guid UserDetailId { get; set; } // Foreign key

        public DateTime DateTimeEntered { get; set; }

    }
}
