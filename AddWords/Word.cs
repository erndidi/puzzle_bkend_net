namespace Puzzle_API.Model
{
    public class Word
    {
        public int Id { get; set; }
        public string Text {get; set; }

        public ICollection<Definition> Definitions { get; set; }
    }
}
