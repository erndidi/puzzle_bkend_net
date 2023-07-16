using Puzzle_API.Model;

namespace Puzzle_API.Repository
{
    public interface IWordRepository : IRepository<Word>
    {
        public Task<Word> GetWord(List<string> strings);
    }
}
