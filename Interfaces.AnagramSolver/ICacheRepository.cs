using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface ICacheRepository
    {
        IList<CacheModel> GetCachedWordsByRequestId(int requestId);
        void InsertWordToCache(int requestId, int anagramID);
    }
}
