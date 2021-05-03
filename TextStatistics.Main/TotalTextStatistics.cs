using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using TextStatistics.Core;
using TextStatistics.Main.Startup;

namespace TextStatistics.Main
{
    public class TotalTextStatistics
    {
        private long numberOfWords = 0;
        private long numberOfLines = 0;
        private List<string> longestWords = new List<string>();
        private List<WordFrequency> topWords = new List<WordFrequency>();

        int numberOfLongestWordsInList = 10;
        int mostFrequentWords = 20;
        string formatString = "{0, -31}  {1, 5}";

        public TotalTextStatistics()
        {
        }

        public void CalculateAllStatistics(params string[] textFiles)
        {
            CalculationOfAllStatistics(textFiles);
            PrintStatistichInConsola();
        }

        private void CalculationOfAllStatistics(string[] textFiles)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            foreach (var textFile in textFiles)
            {
                using (var lifetime = container.BeginLifetimeScope())
                {
                    var containerInstance = lifetime.Resolve<SingleTextStatistics>(new TypedParameter(typeof(string), textFile));
                    containerInstance.CalculationOfStatistics();

                    numberOfWords += containerInstance.NumberOfWords;
                    numberOfLines += containerInstance.NumberOfLines;

                    longestWords.AddRange(containerInstance.LongestWords.Where(x => !longestWords.Any(y => y == x)));

                    foreach (var containerTopWord in containerInstance.TopWords)
                    {
                        if (topWords.Any(item => item.Word == containerTopWord.Word))
                        {
                            topWords.Where(x => x.Word == containerTopWord.Word).Sum(y => y.Frequency = y.Frequency + containerTopWord.Frequency);
                        }
                        else
                        {
                            topWords.Add(containerTopWord);
                        }
                    }
                }
            }
        }

        private void PrintStatistichInConsola()
        {
            var sortedLongestWords = longestWords.OrderByDescending(s => s.Length);
            var sortedTopWords = topWords.OrderByDescending(o => o.Frequency);

            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Calculation summary of all text:");
            Console.WriteLine($"Number of words: {numberOfWords}");
            Console.WriteLine($"Number of lines: {numberOfLines}");
            Console.WriteLine();
            Console.WriteLine($"Most frequent words in all text.");
            Console.WriteLine("-------------");
            foreach (var word in sortedTopWords.Take(mostFrequentWords))
            {
                Console.WriteLine(formatString, word.Word, word.Frequency);
            }
            Console.WriteLine();
            Console.WriteLine($"Longest words in all text.");
            Console.WriteLine("-------------");
            foreach (var word in sortedLongestWords.Take(numberOfLongestWordsInList))
            {
                Console.WriteLine($"{word}");
            }
        }
    }
}
