using Word_Puzzle.Data;
using Word_Puzzle.Model;
using Word_Puzzle.Repository;

namespace Word_Puzzle.Repository
{
    public class WordRepository : Repository<Word>, IWordRepository
    {
        private readonly DataContext _dataContext;
        public WordRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Task<Word> GetWord(List<string> alreadyUsedWords)
        {
            throw new NotImplementedException();
        }

    }
       
}
