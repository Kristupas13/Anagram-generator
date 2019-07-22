using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Services
{
    public interface ICacheService
    {
        void InsertWordToCache(string requestWord, IList<string> anagrams);
        void InsertWordToRequestedWords(string word);
        IList<string> GetAnagramsFromCache(string word);
    }
}
