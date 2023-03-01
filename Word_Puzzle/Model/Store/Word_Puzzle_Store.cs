using Word_Puzzle.Data;
using Word_Puzzle.Model.DTO;

namespace Word_Puzzle.Model.Store
{
    public static class Word_Puzzle_Store
    {
        public static WordDTO GetWord(DataContext dataContext)
        {
            Random rand = new Random();
            // Word word;
            int count = dataContext.Words.Count();
            int rec = rand.Next(1, count);
            Word word = dataContext.Words.Where(w => w.Id == rec).SingleOrDefault();
            Definition def = dataContext.Definitions.Where(d => d.WordId == word.Id).SingleOrDefault();
            WordDTO wordDTO = new WordDTO { Id = word.Id, Text = word.Text };
            wordDTO.Def = new DefinitionDTO { Id = def.Id, Text = def.Text, WordID=wordDTO.Id, IsCorrect=true };
            wordDTO.Decoys = Word_Puzzle_Store.GetDecoys(dataContext, 5, wordDTO.Def.Id);
            return wordDTO;
        }

        private static List<DefinitionDTO> GetDecoys(DataContext dataContext, int total, Guid id)
        {
            List<Definition>  definitions= dataContext.Definitions.Where(d=>d.Id!=id).Take(total).ToList();
            List<DefinitionDTO> defTOs = new List<DefinitionDTO>();
            foreach (Definition definition in definitions)
            {
                defTOs.Add(new DefinitionDTO { Id = definition.Id, Text = definition.Text, IsCorrect = false });
            }
            return defTOs;

         
        }
        
    }
}
