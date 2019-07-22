using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst.Models;
using AnagramGenerator.EF.CodeFirst.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IWordRepository _wordRepository;
        public CacheService(ICacheRepository cacheRepository, IRequestRepository requestRepository, IWordRepository wordRepository)
        {
            _cacheRepository = cacheRepository;
            _requestRepository = requestRepository;
            _wordRepository = wordRepository;
        }
        public void InsertWordToCache(string requestWord, IList<string> anagrams)
        {
            InsertWordToRequestedWords(requestWord);

            bool wordIsCached = _cacheRepository.GetCacheListByRequestWord(requestWord).Any();

            if (!wordIsCached)
            {
                foreach (var item in anagrams)
                {
                    CachedEntity cachedEntity = new CachedEntity()
                    {
                        RequestId = _requestRepository.GetByWord(requestWord).Id,
                        AnagramId = _wordRepository.GetByWord(item).Id
                    };
                    _cacheRepository.Add(cachedEntity);
                }
            }
        }

        public void InsertWordToRequestedWords(string word)
        {

            RequestEntity existingRequestWord = _requestRepository.GetByWord(word);
            if (existingRequestWord == null)
            {
                RequestEntity requestEntity = new RequestEntity()
                {
                    Word = word
                };
                _requestRepository.Add(requestEntity);
            }
        }
        public IList<string> GetAnagramsFromCache(string word)
        {
            if(string.IsNullOrWhiteSpace(word))
                return new List<string>();

            var words = _cacheRepository.GetCacheListByRequestWord(word);

            if (word == null)
                return new List<string>();

            IList<string> wordsFromCache = words.Select(p => p.Anagram.Word).ToList();


            return wordsFromCache;
        }
    }
}
