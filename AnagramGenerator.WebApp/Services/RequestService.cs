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
        public RequestModel GetRequestModel(string word)
        {
            RequestModel requestModel = new RequestModel()
            {
                Word = word
            };
            requestModel = RequestModelContainsWord(word) ? GetExistingRequestModel(word) : AddRequestModel(word);
            return requestModel;
        }
        public bool RequestModelContainsWord(string word)
        {
            return _requestRepository.GetAll().Any(p => p.Word == word);
        }
        public RequestModel GetExistingRequestModel(string word)
        {
            return _requestRepository.GetAll().Where(p => p.Word == word).Select(p => new RequestModel() { Id = p.Id, Word = p.Word }).SingleOrDefault();
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

        public IList<WordModel> ConvertCacheModelToWordModel(IList<CacheModel> cacheModels)
        {

            List<WordModel> wordModels = new List<WordModel>();
            foreach (var item in cacheModels)
            {               
                WordEntity wordEntity = _wordRepository.Get(item.AnagramId);

                WordModel wordModel = new WordModel()
                {
                    Id = wordEntity.Id,
                    Word = wordEntity.Word,
                    SortedWord = wordEntity.SortedWord,
                };
                wordModels.Add(wordModel);
            }
            return wordModels;
        }

        public IList<WordModel> DetectAnagrams(string requestWord)
        {
            IList<WordModel> wordModels = new List<WordModel>();

            string sortedWord = string.Concat(requestWord.ToLower().OrderBy(x => x));

            wordModels = _anagramSolver.GetAnagramsSeperated(sortedWord);

            return wordModels;
        }
        public void TruncateTable(string tableName)
        {
            _managerRepository.TruncateTable(tableName);
        }
        public IList<CacheModel> GetCachedByRequestId(int requestId)
        {
            IList<CacheModel> cachedWords = _cacheRepository.GetAll().Where(p => p.RequestId == requestId).Select(p => new CacheModel() { Id = p.Id, RequestId = p.RequestId, AnagramId = (int)p.AnagramId }).ToList();

            return cachedWords;
        }

    }
}
