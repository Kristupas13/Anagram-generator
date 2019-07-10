using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface IWordRepository

    { 
        Dictionary<string, HashSet<string>> Load();
        Dictionary<string, HashSet<string>> GetDictionary();
        List<string> GetAllWords();
        List<string> FindByWordPart(string wordPart);
        IList<string> GetAnagrams(string sortedWord);
        IList<string> LoadWords(int page);

    }
}
