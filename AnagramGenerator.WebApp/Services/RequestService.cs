using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Services
{
    public class RequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramSolver _anagramSolver;
        private readonly IManagerRepository _managerRepository;
        public RequestService(IRequestRepository requestRepository, IWordRepository wordRepository, IAnagramSolver anagramSolver, IManagerRepository managerRepository)
        {
            _requestRepository = requestRepository;
            _wordRepository = wordRepository;
            _anagramSolver = anagramSolver;
            _managerRepository = managerRepository;
        }
        public RequestModel RequestedWord(string word)
        {

            bool requestedWordExists = _requestRepository.Exists(word);

            if (!requestedWordExists)
            {
                _requestRepository.Add(word);
            }

            RequestModel request = _requestRepository.ToModel(word);

            return request;
        }
        public IList<WordModel> ConvertCacheModelToWordModel(IList<CacheModel> cacheModels)
        {
            List<WordModel> wordModels = new List<WordModel>();
            foreach (var item in cacheModels)
            {
                WordModel wm = _wordRepository.GetWordModel(item.AnagramId);
                wordModels.Add(wm);
            }
            return wordModels;
        }

        public IList<WordModel> FindAnagrams(string requestWord)
        {
            IList<WordModel> wordModels = new List<WordModel>();

            wordModels = _anagramSolver.GetAnagramsSeperated(requestWord);

            return wordModels;
        }
        public void TruncateTable(string tableName)
        {
            _managerRepository.TruncateTable(tableName);
        }
    }
}
