namespace Puzzle_API.Model
{
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

        public string? UserName { get; set; }

        public UserSession UserSession { get; set; } // Navigation property


    }
}
