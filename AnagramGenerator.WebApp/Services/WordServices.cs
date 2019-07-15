using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.DataAccess;
using Microsoft.AspNetCore.Http;
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
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramSolver _anagramSolver;

        public WordServices(ICacheRepository cacheRepository, IUserLogRepository userLogRepository, IWordRepository wordRepository, IAnagramSolver anagramSolver)
        {
            _cacheRepository = cacheRepository;
            _userLogRepository = userLogRepository;
            _wordRepository = wordRepository;
            _anagramSolver = anagramSolver;

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
        public IList<WordModel> GetWordModel(string phrase)
        {
            List<WordModel> allWords = new List<WordModel>();
            var seperatedWords = phrase.Split(" ");
            foreach(var item in seperatedWords)
            {
                WordModel wordModel = _wordRepository.GetWordModel(item);
                if(wordModel.Word == null)
                {
                    wordModel.Id = 0;
                    wordModel.Word = item;
                }
                allWords.Add(wordModel); 
            }
            return allWords;
        }
        public WordModel IDToWordRelation(int ID)
        {
            return _wordRepository.GetWordModel(ID);
        }
        public IList<WordModel> ConvertCachedToWords(IList<CacheModel> cacheModels)
        {
            List<WordModel> wordModels = new List<WordModel>();
            foreach (var item in cacheModels)
            {
                WordModel wm = IDToWordRelation(item.AnagramId);
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

        }
    }
}
