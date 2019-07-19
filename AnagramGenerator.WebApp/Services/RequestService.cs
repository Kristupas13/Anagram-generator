using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst.Models;

namespace AnagramGenerator.WebApp.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramSolver _anagramSolver;
        private readonly IManagerRepository _managerRepository;
        private readonly ICacheRepository _cacheRepository;
        public RequestService(IRequestRepository requestRepository, IWordRepository wordRepository, IAnagramSolver anagramSolver, IManagerRepository managerRepository, ICacheRepository cacheRepository)
        {
            _requestRepository = requestRepository;
            _wordRepository = wordRepository;
            _anagramSolver = anagramSolver;
            _managerRepository = managerRepository;
            _cacheRepository = cacheRepository;
        }
        public RequestModel AddRequestModel(string word)
        {
            RequestModel requestModel = new RequestModel()
            {
                Word = word
            };
            requestModel.Id = _requestRepository.Add(new RequestEntity() { Word = word});
            return requestModel;
        }

        public IList<string> DetectAnagrams(string requestWord)
        {
            IList<string> words =  _anagramSolver.GetAnagramsSeperated(requestWord);

            return words;
        }

        public void TruncateTable(string tableName)
        {
            _managerRepository.TruncateTable(tableName);
        }
        public IList<string> GetAnagramsFromCache(string word)
        {

            IList<string> words = _cacheRepository.GetAll().Where(p => p.Request.Word == word).Select(p => p.Anagram.Word).ToList();

            return words;
        }

    }
}
