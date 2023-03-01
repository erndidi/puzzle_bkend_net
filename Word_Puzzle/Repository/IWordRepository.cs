using Word_Puzzle.Model;

namespace Word_Puzzle.Repository
{
    public interface IWordRepository : IRepository<Word>
    {
        public Task<Word> GetWord(List<string> strings);
    }
}
