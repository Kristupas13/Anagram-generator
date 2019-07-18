using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Services
{
    public class CacheService
    {
        private readonly ICacheRepository _cacheRepository;
        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }
        public void InsertWordToCache(int requestId, IList<WordModel> anagrams)
        {
            foreach (var item in anagrams)
            {
                _cacheRepository.InsertWordToCache(requestId, item.Id);
            }
        }
        public IList<CacheModel> GetCachedByRequestId(int requestId)
        {
            List<CacheModel> cachedWords = new List<CacheModel>();
            cachedWords.AddRange(_cacheRepository.GetCachedWordsByRequestId(requestId));
            return cachedWords;
        }
    }
}
