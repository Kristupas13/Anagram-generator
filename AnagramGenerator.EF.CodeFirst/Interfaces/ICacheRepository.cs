using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Interfaces
{
    public interface ICacheRepository
    {
        IList<CachedEntity> GetAll();
        CachedEntity Get(int cacheId);
        int Add(CachedEntity cachedEntity);
        CachedEntity Update(CachedEntity cachedEntity);
        bool Contains(CachedEntity cachedEntity);




        IList<CacheModel> GetCachedWordsByRequestId(int requestId);
        void InsertWordToCache(int requestId, int anagramID);
        bool WordExists(int requestId);
    }
}
