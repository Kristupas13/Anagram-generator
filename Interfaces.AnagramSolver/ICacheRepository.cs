using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface ICacheRepository
    {
        IList<CacheModel> CheckCached(string word);
        void InsertWordToCache(string word, int anagramID);
    }
}
