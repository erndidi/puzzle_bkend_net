namespace Puzzle_API.Model
{
    public class UserWord
    {
        public Guid Id { get; set; }
        public UserDetail UserDetail { get; set; }
        public string UsedWord { get; set; }
    }
}
