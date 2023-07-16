namespace Puzzle_API.Logging
{
    public class LoggerProvider : ILoggerProvider
    {
        public LoggerProvider() { }

        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void LogError(string content)
        {
            Console.WriteLine(content);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
