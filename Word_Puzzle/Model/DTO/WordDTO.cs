namespace Puzzle_API.Model.DTO
{
    public class WordDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string? UsedWord { get; set; }

        // public Definition Definitions { get; set; }  
        public List<DefinitionDTO> Definitions = new List<DefinitionDTO>();
    }
}
