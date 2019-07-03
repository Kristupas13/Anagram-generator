using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Interfaces.AnagramSolver;

namespace Implementation.AnagramSolver
{
    public class AnagramSolver : IAnagramSolver
    {
        readonly List<string> anagramList = new List<string>();
        private readonly IWordRepository _wordRepository;
        bool endOfRecursion = false;
        private readonly Dictionary<string, HashSet<string>> words;

        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            words = _wordRepository.GetWords();

        }
        public IList<string> GetAnagramsSeperated(string myWords) // only checks word by word  O(n)
        {
            IList<string> anagramWords = new List<string>();
            bool matchFound = false;
            string[] myWordsSplited = myWords.Split(" ");
            foreach (var item in myWordsSplited)
            {
                matchFound = false;
                foreach (var contents in words.Keys)
                {
                    foreach (var listMember in words[contents])
                    {
                        bool isAnagram = CheckIfEqual(SymbolFrequency(listMember), SymbolFrequency(item));
                        bool areNotEqual = !(item.Equals(listMember));

                        if (isAnagram && areNotEqual)
                        {
                            matchFound = true;
                            anagramWords.Add(listMember);
                            break;
                        }
                    }
                    if (matchFound) // after first find break
                        break;
                }
                if (!matchFound) // if one of the words didint have anagram return empty
                return new List<string>();
            }

         return anagramWords;
               
        }
        public IList<string> GetAnagrams(string myWords)    // checks connected words  (not working properly)
        {
            int minSymbols = 4; // temp
            int maxWords = 5;   // temp


            IList<string> anagramList = RecursiveCheck(SymbolFrequency(myWords), minSymbols, maxWords);

            return anagramList;
        }


        private IList<string> RecursiveCheck(Dictionary<char, int> frequency, int minimumLength, int maxWords)
        {
            Dictionary<char, int> currentWordFrequency = new Dictionary<char, int>();
            foreach (var contents in words.Keys)
            {
                foreach (var listMember in words[contents])
                {
                    if (listMember.Length >= minimumLength)
                    {
                        currentWordFrequency = SymbolFrequency(listMember);

                        if (CheckIfContains(currentWordFrequency, frequency))
                        {
                            // word counter ++

                            var copied = new Dictionary<char, int>(frequency);

                            foreach (var key in currentWordFrequency.Keys.ToList())
                            {
                                copied[key] -= currentWordFrequency[key];
                            }

                            anagramList.Add(listMember);
                            var allLetersUsed = copied.All(x => x.Value == 0);
                            if (allLetersUsed || endOfRecursion)
                            {
                                endOfRecursion = true;
                                return anagramList;
                            }
                            RecursiveCheck(copied, minimumLength, maxWords);
                            anagramList.Remove(listMember);
                            // word counter --
                        }
                    }



                }
            }

            return anagramList;
        }

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
    }
}
