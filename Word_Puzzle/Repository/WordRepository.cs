using Puzzle_API.Data;
using Puzzle_API.Model;
using Puzzle_API.Repository;

namespace Puzzle_API.Repository
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
