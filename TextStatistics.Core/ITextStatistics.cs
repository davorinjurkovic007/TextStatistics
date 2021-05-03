using System.Collections.Generic;

namespace TextStatistics.Core
{
    public interface ITextStatistics
    {
        List<string> LongestWords(int n);
        List<WordFrequency> TopWords(int n);
        void CalculateStatistics(string inputText);
        long NumberOfLines();
        long NumberOfWords();
    }
}