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
        private readonly IAnagramSolver _anagramSolver;
        private readonly IWordRepository _wordRepository;
        private readonly IManagerRepository _managerRepository;
        public RequestService(IAnagramSolver anagramSolver, IWordRepository wordRepository , IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
            _wordRepository = wordRepository;
            _anagramSolver = anagramSolver;
        }
        public IList<string> DetectAnagrams(string requestWord)
        {
            IList<string> words =  _anagramSolver.GetAnagramsSeperated(requestWord);

            return words;
        }

        public IList<string> Find(string word)
        {
            IList<string> wordsToDesplay = new List<string>();


           // wordsToDesplay = _wordRepository.GetAll().Where(p => p.Word == word).Select(p => p.Word).ToList();
           // huge memory leak

            wordsToDesplay = _wordRepository.GetListByPartWord(word).Select(p => p.Word).ToList();

            return wordsToDesplay;
        }

        public IList<string> LoadWords(int page)
        {
            return _managerRepository.LoadWords(page);
        }

    }
}
