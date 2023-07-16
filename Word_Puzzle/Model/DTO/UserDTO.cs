namespace Puzzle_API.Model.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string? FirstName { get; set; }

        public string? Lastname { get; set; }


        public string? GoogleId { get; set; }

        public string? FacebookId { get; set; }

        public string SessionId { get; set; }
        public UserDetail UserDetail { get; set; }

        public string UserName { get; set; }

        public string UsedWord { get; set; }
        public int Score { get; set; }




    }
}
