namespace Word_Puzzle.Model.DTO
{
    public class DefinitionDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public int WordID { get; set; }

        public bool IsCorrect { get; set; }
    }
}
