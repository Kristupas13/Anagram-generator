using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.Implementations
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly ITextRepository _wordRepository;

        public AnagramSolver(ITextRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IList<WordModel> GetAnagramsSeperated(string myWords)
        {
            var seperatedWords = myWords.ToLower().Split(" ");
            List<WordModel> anagrams = new List<WordModel>();
            foreach (var item in seperatedWords)
            {
                string sortedWord = String.Concat(item.ToLower().OrderBy(c => c));

                IList<WordModel> wordAnagrams = _wordRepository.GetAnagrams(sortedWord);
                if (wordAnagrams != null)
                {
                    anagrams.AddRange(wordAnagrams);
                }

            }
            return anagrams;
        }
    }
    /*
        public Dictionary<char, int> SymbolFrequency(string inputText)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();
            inputText = string.Join("", inputText.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)); // remove whitespaces from inputString

            foreach (var c in inputText)
            {
                if (frequency.ContainsKey(c))
                    frequency[c]++;
                else
                    frequency[c] = 1;
            }
            return frequency;
        }

        private bool CheckIfEqual(Dictionary<char, int> currentWord, Dictionary<char, int> inputWords)
        {
            if (currentWord.Count == inputWords.Count && !currentWord.Except(inputWords).Any())
            {
                return true;
            }
            return false;
        }
        private bool CheckIfContains(Dictionary<char, int> currentWord, Dictionary<char, int> inputWords)
        {
            var allexist = currentWord.All(x => inputWords.ContainsKey(x.Key) && x.Value <= inputWords[x.Key]);
            return allexist;
        }
        private bool CheckIfLengthNotLower(int wordLength, int minimumLength)
        {
            return (wordLength >= minimumLength);
        }

        public IList<string> GetAnagrams(string myWords)    // checks connected words  (not working properly)
        {
            int startIndex = 0;
            List<string> inputWords = myWords.Split(" ").ToList();

            int minLength = MainApp.Config.MinWordLength;
            int maxAnagrams = MainApp.Config.MaxWords;

            Console.WriteLine("Please wait");

            IList<string> anagramList = RecursiveCheck(SymbolFrequency(myWords), inputWords, minLength, maxAnagrams, startIndex);

            return anagramList;
        }

        // remainder of symbol frequency  // input phrase          //minLength, maxWords appconfig //index where to continue seaching
        private IList<string> RecursiveCheck(Dictionary<char, int> frequency, List<string> inputWords, int minimumLength, int maxWords, int currentIndex)
        {
            Dictionary<char, int> currentWordFrequency = new Dictionary<char, int>();
            for (int i = currentIndex; i < allWords.Count; i++)
            {
                bool isSameWord = inputWords.Contains(allWords[i]);

                if (CheckIfLengthNotLower(allWords[i].Length, minimumLength) && !isSameWord)
                {
                    currentWordFrequency = SymbolFrequency(allWords[i]);

                    if (CheckIfContains(currentWordFrequency, frequency))
                    {


                        var copied = new Dictionary<char, int>(frequency);

                        foreach (var key in currentWordFrequency.Keys.ToList())
                        {
                            copied[key] -= currentWordFrequency[key];
                        }


                        anagramList.Add(allWords[i]);
                        var allLetersUsed = copied.All(x => x.Value == 0);
                        if (allLetersUsed)
                        {
                            endOfRecursion = true;
                            return anagramList;
                        }
                        else
                            RecursiveCheck(copied, inputWords, minimumLength, maxWords, i + 1);
                        if (endOfRecursion)
                        {
                            return anagramList;
                        }

                        anagramList.Remove(allWords[i]);
                    }
                }

            }

            return anagramList;
        }*/

}
