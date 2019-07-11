using AnagramGenerator.Contracts;
using AnagramGenerator.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Services
{
    public class WordServices
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IAnagramRepository _anagramRepository;

        public WordServices(ICacheRepository cacheRepository, IUserLogRepository userLogRepository, IAnagramRepository anagramRepository)
        {
            _cacheRepository = cacheRepository;
            _userLogRepository = userLogRepository;
            _anagramRepository = anagramRepository;

        }
        public IList<WordModel> CheckCached(IList<WordModel> words)
        {
            List<WordModel> cachedWords = new List<WordModel>();
            foreach(var word in words)
            {
                cachedWords.AddRange(_cacheRepository.CheckCached(word));
            }
            return cachedWords;
        }
        public void InsertWordToCache(IList<WordModel> words, IList<WordModel> anagrams)
        {
            foreach (var item in words)
            {
                _cacheRepository.InsertWordToCache(item, anagrams);
            }
        }
        public IList<UserLogModel> GetUserLog(string ip)
        {
            return _userLogRepository.GetUserLog(ip);
        }
        public void InsertToUserLog(IList<WordModel> searchedWord, string IpAddress)
        {
            foreach (var item in searchedWord)
            {
                _userLogRepository.InsertToUserLog(item, IpAddress);
            }
        }
        public IList<WordModel> GetWordModel(string phrase)
        {
            List<WordModel> allWords = new List<WordModel>();
            var seperatedWords = phrase.Split(" ");
            foreach(var item in seperatedWords)
            {
                WordModel wordModel = _anagramRepository.GetWordModel(item);
                if(wordModel.Word == null)
                {
                    wordModel.ID = 0;
                    wordModel.Word = item;
                }
                allWords.Add(wordModel); 
            }
            return allWords;
        }
    }
}
