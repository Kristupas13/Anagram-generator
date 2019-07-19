using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Interfaces
{
    public interface ITextRepository
    {
        void Add(WordEntity wordEntity);
        void Remove(WordEntity wordEntity);
        void Update(WordEntity word);


        Dictionary<string, HashSet<string>> Load();
        List<string> GetWords();
        List<string> Find(string wordPart);
        IList<WordModel> GetAnagrams(string sortedWord);
        IList<string> LoadWords(int page);

    }
}
