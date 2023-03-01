namespace Word_Puzzle.Model.DTO
{
    public class WordDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DefinitionDTO Def { get; set; }  
        public List<DefinitionDTO> Decoys = new List<DefinitionDTO>();
    }
}
