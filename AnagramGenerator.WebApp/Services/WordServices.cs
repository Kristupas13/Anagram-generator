using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
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
        public IList<WordModel> CheckCached(string phrase)
        {
            List<WordModel> cachedWords = new List<WordModel>();
            var splited = phrase.Split(" ");
            foreach(var word in splited)
            {
                cachedWords.AddRange(_cacheRepository.CheckCached(word));
            }
            return cachedWords;
        }
        public void InsertWordToCache(string words, IList<WordModel> anagrams)
        {
            var splited = words.Split(" ");

            foreach (var item in splited)
            {
                _cacheRepository.InsertWordToCache(item, anagrams);
            }
        }
        public IList<UserLogModel> GetUserLog(string ip)
        {
            return _userLogRepository.GetUserLog(ip);
        }
        public void InsertToUserLog(string searchedWord, string IpAddress)
        {
         _userLogRepository.InsertToUserLog(searchedWord, IpAddress);

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
                    wordModel.Id = 0;
                    wordModel.Word = item;
                }
                allWords.Add(wordModel); 
            }
            return allWords;
        }
    }
}
