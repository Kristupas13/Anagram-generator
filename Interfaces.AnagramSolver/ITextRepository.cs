using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface ITextRepository

    {
        void Add(string word);
        void Remove(WordModel word);
        Dictionary<string, HashSet<string>> Load();
        List<string> GetWords();
        List<string> Find(string wordPart);
        IList<WordModel> GetAnagrams(string sortedWord);
        IList<string> LoadWords(int page);

    }
}
