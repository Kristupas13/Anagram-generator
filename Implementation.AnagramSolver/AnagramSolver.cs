using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.AnagramSolver;

namespace Implementation.AnagramSolver
{
    public class AnagramSolver : IAnagramSolver
    {
        private IWordRepository _wordRepository;
        private Dictionary<string, HashSet<string>> words;
        private Dictionary<char, int> symbolCount = new Dictionary<char, int>();


        public AnagramSolver(IWordRepository wordRepository, string inputText)
        {
            _wordRepository = wordRepository;
            words = _wordRepository.getWordRepository();
            SymbolFrequency(inputText);
            // temporary
            foreach (var contents in words.Keys)
            {

                foreach (var listMember in words[contents])
                {
                    Console.WriteLine("Key : " + contents + " member :" + listMember);
                }
            }
            

        }
        public void SymbolFrequency(string inputText)
        {
            foreach (var c in inputText)
            {
                if (symbolCount.ContainsKey(c))
                    symbolCount[c]++;
                else
                    symbolCount[c] = 1;
            }

          /* foreach(var s in symbolCount)
            {
                Console.WriteLine(s.Key + " has a value of " + s.Value);
            }*/
        }
        public IList<string> GetAnagrams(string myWords)
        {

            return new List<string>();
        }

    }
}
