using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.WebApp.Services
{
    public class WordService
    {
        private readonly IWordRepository _wordRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly ITextRepository _textRepository;


        public WordService(ITextRepository textRepository, IWordRepository wordRepository, IManagerRepository managerRepository)
        {
            _textRepository = textRepository;
            _wordRepository = wordRepository;
            _managerRepository = managerRepository;

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
                _textRepository.Remove(_wordRepository.ToWordModel(word));
            }

        }
        public void EditWord(string oldWord, string newWord, string ip)
        {
            bool wordExists = _managerRepository.WordExists(oldWord);

            if(wordExists)
            {
                _textRepository.Edit(_wordRepository.ToWordModel(oldWord), newWord);
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
