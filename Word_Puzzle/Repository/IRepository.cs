namespace Puzzle_API.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<T> Get(List<string> strings);

    }
}
