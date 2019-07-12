using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.Implementations
{
    public class TextWordRepository : IWordRepository
    {
        private readonly Dictionary<string, HashSet<string>> wordsByPart;
        private readonly IList<string> sortedWords;
        string Path { get; set; }

        public TextWordRepository()
        {
            Path = @"C:\Users\kristupas\Desktop\UpdatedDataManipulation\Cloned\Anagram-generator\MainApp\zodynas.txt";
            wordsByPart = Load();
        }
        public Dictionary<string, HashSet<string>> Load()
        {
            Dictionary<string, HashSet<string>> words = new Dictionary<string, HashSet<string>>();
            try
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        char[] delimiterChars = { '\t' };
                        string[] seperatedWords = line.Split(delimiterChars); // 0 - vardininkas, 1 - kalbos dalis, 2 - linksnis, 3 - skaicius
                        var vardininkas = seperatedWords[0];
                        var kalbosDalis = seperatedWords[1];
                        var linksnis = seperatedWords[2];
                        if (words.ContainsKey(kalbosDalis))
                        {
                            words[kalbosDalis].Add(vardininkas);
                            words[kalbosDalis].Add(linksnis);
                        }
                        else
                        {
                            words[kalbosDalis] = new HashSet<string> { vardininkas, linksnis };
                        }
                    }

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Your file could not be found!");
                Console.WriteLine(e.Message);
            }

            return words;
        }



        public Dictionary<string, HashSet<string>> GetDictionary()
        {
            return wordsByPart;
        }

        public List<string> GetAllWords()
        {
            List<string> allWords = new List<string>();
            foreach (var contents in wordsByPart.Keys)
            {
                foreach (var listMember in wordsByPart[contents])
                {
                    allWords.Add(listMember);
                }
            }

            return allWords;
        }

        public List<string> FindByWordPart(string part)
        {
            throw new System.NotImplementedException();
        }
        public IList<WordModel> GetAnagrams(string sortedWord)
        {
            IList<WordModel> anagrams = new List<WordModel>();
            foreach (var content in wordsByPart.Keys)
            {
                foreach (var item in wordsByPart[content])
                {
                    bool same = sortedWord.OrderBy(c => c)
                        .SequenceEqual(item.OrderBy(c => c));
                    if (same)
                    {
                        WordModel wm = new WordModel()
                        {
                            Id = 0,
                            Word = item
                        };
                        anagrams.Add(wm);
                    }
                }
            }

            return anagrams;
        }

        public IList<string> LoadWords(int page)
        {
            throw new NotImplementedException();
        }
    }
}
