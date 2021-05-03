using System;
using System.Collections.Generic;
using TextStatistics.Core;
using TextStatistics.Data;

namespace TextStatistics.Main
{
    public class SingleTextStatistics
    {
        private readonly string nameOfFile;
        private ITextFileReader textFileReader;
        private ITextStatistics textStatistics;
        private string textFromFile;
        private long numberOfWords;
        private long numberOfLines;
        private List<string> longestWords = new List<string>();
        private List<WordFrequency> topWords = new List<WordFrequency>();

        int numberOfLongestWordsInList = 10;
        int mostFrequentWords = 20;
        string formatString = "{0, -31}  {1, 5}";

        /// <summary>
        /// Return total number of words in the text
        /// </summary>
        public long NumberOfWords => numberOfWords;

        /// <summary>
        /// Return total number of lines in the text
        /// </summary>
        public long NumberOfLines => numberOfLines;

        /// <summary>
        /// Return a list of the longest words of the text
        /// </summary>
        public List<string> LongestWords => longestWords;

        /// <summary>
        /// Return a list of the most frequented words of the text
        /// </summary>
        public List<WordFrequency> TopWords => topWords;

        public SingleTextStatistics(string nameOfFile, ITextFileReader textFileReader, ITextStatistics textStatistics)
        {
            this.nameOfFile = nameOfFile;
            this.textFileReader = textFileReader;
            this.textStatistics = textStatistics;
        }

        public void CalculationOfStatistics()
        {
            ReadTextFile();

            CalculateStatistics();
        }

        public void ShowStatistics()
        {
            CalculationOfStatistics();

            PrintStatisticsInConsola();
        }

        private void ReadTextFile()
        {
            textFileReader.ReadTextFile(nameOfFile);
            textFromFile = textFileReader.TextOutput;
        }

        private void CalculateStatistics()
        {
            textStatistics.CalculateStatistics(textFromFile);

            numberOfWords = textStatistics.NumberOfWords();
            numberOfLines = textStatistics.NumberOfLines();
            longestWords = textStatistics.LongestWords(numberOfLongestWordsInList);
            topWords = textStatistics.TopWords(mostFrequentWords);
        }

        private void PrintStatisticsInConsola()
        {
            Console.WriteLine($"-----------------Statistics for {nameOfFile}");
            Console.WriteLine($"Number of words: {numberOfWords}");
            Console.WriteLine($"Number of lines: {numberOfLines}");
            Console.WriteLine($"Most frequent words in text.");
            Console.WriteLine("-------------");
            foreach (var word in topWords)
            {
                Console.WriteLine(formatString, word.Word, word.Frequency);
            }
            Console.WriteLine($"Longest words in text.");
            Console.WriteLine("-------------");
            foreach (var word in longestWords)
            {
                Console.WriteLine($"{word}");
            }
        }
    }
}
