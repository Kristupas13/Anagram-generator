using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.WebApp.Services
{
    public class WordServices
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IAnagramSolver _anagramSolver;
        private readonly ITextRepository _textRepository;

        public WordServices(ITextRepository textRepository, ICacheRepository cacheRepository, IUserLogRepository userLogRepository, IWordRepository wordRepository, IManagerRepository managerRepository ,IAnagramSolver anagramSolver)
        {
            _textRepository = textRepository;
            _cacheRepository = cacheRepository;
            _userLogRepository = userLogRepository;
            _wordRepository = wordRepository;
            _anagramSolver = anagramSolver;
            _managerRepository = managerRepository;

        }
        public IList<CacheModel> CheckCached(string phrase)
        {
            List<CacheModel> cachedWords = new List<CacheModel>();
            cachedWords.AddRange(_cacheRepository.CheckCached(phrase));
            return cachedWords;
        }
        public void InsertWordToCache(string word, IList<WordModel> anagrams)
        {
            foreach(var item in anagrams)
            {
                _cacheRepository.InsertWordToCache(word, item.Id);
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
        public IList<WordModel> ConvertCachedToWords(IList<CacheModel> cacheModels)
        {
            List<WordModel> wordModels = new List<WordModel>();
            foreach (var item in cacheModels)
            {
                WordModel wm = _wordRepository.GetWordModel(item.AnagramId);
                wordModels.Add(wm);
            }
            return wordModels;
        }
        public IList<WordModel> FindAnagrams(string phrase)
        {
            IList<WordModel> wordModels = new List<WordModel>();

            wordModels = _anagramSolver.GetAnagramsSeperated(phrase);
            if (wordModels.Any())
            {
                InsertWordToCache(phrase, wordModels);
            }

            return wordModels;
        }
        public void TruncateTable(string tableName)
        {
            _managerRepository.TruncateTable(tableName);
        }
        public bool CheckIPLimit(string ip)
        {
            return _userLogRepository.UserIPLimit(ip);
        }
        public void AddWord(string word, string ip)
        {
            bool wordExists = _managerRepository.WordExists(word);

            if(!wordExists)
            _textRepository.Add(word);
        }
        public void RemoveWord(string word, string ip)
        {
            bool wordExists = _managerRepository.WordExists(word);

            if (wordExists)
            {
                _textRepository.Remove(_wordRepository.GetWordModel(word));
            }
      
        }
        public IList<string> Find(string word)
        {
            return _textRepository.Find(word);
        }
        public IList<string> LoadWords(int page)
        {
            return _textRepository.LoadWords(page);
        }
    }
}
