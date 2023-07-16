using System.ComponentModel.DataAnnotations.Schema;

namespace Puzzle_API.Model
{
    public class Definition
    {
        public Guid Id { get; set; }
        [ForeignKey("Words")]
        public int WordId { get; set; }
        public string Text { get; set; }

        public Word Word { get; set; }
    }
}
