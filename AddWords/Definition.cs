namespace Puzzle_API.Model
{
    public class Definition
    {
        public Guid Id { get; set; }        
        public string Text { get; set; }

        public Word Word { get; set; }
    }
}
