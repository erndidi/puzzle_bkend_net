namespace Puzzle_API.Model
{
    public class Friends
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key for the user
        public int FriendId { get; set; } // Foreign key for the friend (also a user)
    }
}
