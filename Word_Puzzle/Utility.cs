namespace Puzzle_API
{
    public static class Utility
    {
        public static int GetWordScore(string word, int seconds)
        {
            char[] letters = word.ToCharArray();
            return letters.Length * seconds;
        }
    }
}
