using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IWordRepository

    { 
        Dictionary<string, HashSet<string>> Load();
        Dictionary<string, HashSet<string>> GetDictionary();
        List<string> GetAllWords();
        List<string> FindByWordPart(string wordPart);
        IList<WordModel> GetAnagrams(string sortedWord);
        IList<string> LoadWords(int page);

    }
}
