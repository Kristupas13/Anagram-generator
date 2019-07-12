using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface ICacheRepository
    {
        IList<WordModel> CheckCached(string word);
        void InsertWordToCache(string word, IList<WordModel> anagrams);
    }
}
