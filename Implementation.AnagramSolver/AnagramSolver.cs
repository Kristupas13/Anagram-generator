using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces.AnagramSolver;

namespace Implementation.AnagramSolver
{
    public class AnagramSolver : IAnagramSolver
    {
        private IWordRepository _wordRepository;
        private Dictionary<string, HashSet<string>> words;



        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            words = _wordRepository.GetWords();
            /* temporary
            foreach (var contents in words.Keys)
            {

                foreach (var listMember in words[contents])
                {
                    Console.WriteLine("Key : " + contents + " member :" + listMember);
                }
            }
            */
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
            /*
           foreach(var s in symbolCount)
            {
                Console.WriteLine(s.Key + " has a value of " + s.Value);
            }*/
            return frequency;
        }
        //public IList<string> GetAnagrams(string myWords)    // checks connected words
        //{
        //    int minSymbols = 3; // temporary
        //    int maxWords = 3;   // temporary
        //    Dictionary<char, int> currentWordFrequency = SymbolFrequency(myWords);

        //    SymbolFrequency(myWords);

        //    double total = symbolCount.Sum(x => x.Value);
        //    int totalWords = words.Values.Sum(x => x.Count);
        //    Console.WriteLine("Total amount of symbols in phrase: {0} ", total);

        //    Console.WriteLine("Total amount of words in dictionary: {0}", totalWords);

        //    /*
        //     * while symbolCount = 0 or end of words values
        //     * SymbolFrequency(Word from dictionary);
        //     * CheckIfSubString(currentWordFrequency, symbolCount) 
        //     * 
        //     * 
        //     * 
        //     * 
        //     */

        //    return new List<string>();
        //}

        public IList<string> GetAnagramsSimple(string myWords) // only checks word by word
        {
            List<string> wordsThatMatch = new List<string>();
            string[] myWordsSplited = myWords.Split(" ");
            foreach(var item in myWordsSplited)
            {
                Dictionary<char, int> currentWordFrequency = SymbolFrequency(item);

               
                foreach (var contents in words.Keys)
                {
                    foreach (var listMember in words[contents])
                    {
                        Dictionary<char, int> symbolCount = SymbolFrequency(listMember);
                        if (CheckIfSubString(symbolCount, currentWordFrequency))
                        {
                            wordsThatMatch.Add(listMember);
                        }

                    }
                }
            }
            return wordsThatMatch;

        }

        public bool CheckIfSubString(Dictionary<char, int> currentWord, Dictionary<char, int> inputWords)
        {
            if (currentWord.Count == inputWords.Count && !currentWord.Except(inputWords).Any())
            {
                return true;
            }
            return false;
        }
    }
}
