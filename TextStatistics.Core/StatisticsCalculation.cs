using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextStatistics.Core
{
    public class StatisticsCalculation : ITextStatistics
    {
        private const string PATTERN = @"[\p{L}\p{M}*\d+][\.|\,]?[\p{L}\p{M}*\-'’]*";
        private string textInString;

        private Dictionary<string, long> wordsFrequency = new Dictionary<string, long>();
        private Dictionary<string, long> wordsLong = new Dictionary<string, long>();

        private List<WordFrequency> mostFrequentWords;
        private List<string> longestWords;

        public StatisticsCalculation()
        {
        }

        public void CalculateStatistics(string inputtext)
        {
           textInString = inputtext;

            try
            {
                Calculation();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Calculation()
        {
            if (textInString == null) throw new ArgumentNullException("String is NULL");

            foreach (Match match in Regex.Matches(textInString, PATTERN, RegexOptions.IgnoreCase))
            {
                string word = match.Value;
                // TODO: Pattern need to refine to avoid this part
                string lastChar = word.Substring(word.Length - 1, 1);
                if (lastChar == "," || lastChar == ".")
                {
                    word = word.Remove(word.Length - 1, 1);
                }

                long output;
                if (!string.IsNullOrEmpty(word))
                {
                    if (wordsFrequency.TryGetValue(word, out output))
                    {
                        wordsFrequency[word]++;
                    }
                    else
                    {
                        wordsFrequency.Add(word, 1);
                        wordsLong.Add(word, word.Length);

                    }
                }
            }
        }

        public List<WordFrequency> TopWords(int n)
        {
            mostFrequentWords = new List<WordFrequency>();
            
            foreach (KeyValuePair<string, long> wordFrequency in wordsFrequency.OrderByDescending(key => key.Value).Take(n))
            {
                mostFrequentWords.Add(new WordFrequency 
                {
                    Word = wordFrequency.Key,
                    Frequency = wordFrequency.Value
                });
            }
            return mostFrequentWords;
        }

        public List<string> LongestWords(int n)
        {
            longestWords = new List<string>();
            
            foreach (KeyValuePair<string, long> wordLong in wordsLong.OrderByDescending(key => key.Value).Take(n))
            { 
                longestWords.Add(wordLong.Key);
            }

            return longestWords;
        }

        public long NumberOfWords()
        {
            return wordsLong.Count;
        }

        public long NumberOfLines()
        {
            long numberOfLines = 0;
            try
            {
                numberOfLines = CountLinesInString(textInString);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return numberOfLines;
        }

        private long CountLinesInString(string textToCount)
        {
            if (textToCount == null) throw new ArgumentNullException("String is NULL");

            long counter = 1;
            string[] temporaryString = textToCount.Split(new string[] { "\n" }, StringSplitOptions.None);
            if (temporaryString.Length > 0)
            {
                counter = temporaryString.Length;
            }
            return counter;
        }
    }
}
