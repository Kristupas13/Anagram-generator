using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst.Models;

namespace AnagramGenerator.WebApp.Services
{
    public class ModificationService : IModificationService
    {
        private readonly IWordRepository _wordRepository;
        private readonly ITextRepository _textRepository;
        private readonly ICacheRepository _cacheRepository;


        public ModificationService(ITextRepository textRepository, IWordRepository wordRepository, ICacheRepository cacheRepository) 
        {
            _textRepository = textRepository;
            _wordRepository = wordRepository;
            _cacheRepository = cacheRepository;

        }
        public bool AddWord(string word, string ip)
        {
            bool wordExists = _wordRepository.WordExists(word);

            if(!wordExists)
            {
                _textRepository.Add(word);
                return true;
            }
            
            return false;
        }
        public bool RemoveWord(string word, string ip)
        {
            bool wordExists = _wordRepository.WordExists(word);

            if (wordExists)
            {
                _textRepository.Remove(_wordRepository.ToWordModel(word));
                return true;
            }
            return false;
        }
        public bool EditWord(string oldWord, string newWord, string ip)
        {
            bool wordExists = _wordRepository.WordExists(oldWord);
            bool newWordExists = _wordRepository.WordExists(newWord);

            if(wordExists && !newWordExists)
            {
                _textRepository.Edit(_wordRepository.ToWordModel(oldWord), newWord);
                return true;
            }
            return false;
        }
        public IList<string> Find(string word)
        {
            return _textRepository.Find(word);
        }
        public IList<string> LoadWords(int page)
        {
            return _textRepository.LoadWords(page);
        }
        public bool WordExists(string word)
        {
            bool wordExists = _wordRepository.WordExists(word);
            return wordExists;
        }
        public void InsertWordToCache(int requestId, IList<WordModel> anagrams)
        {
            if (!(_cacheRepository.GetAll().Any(p => p.RequestId == requestId)))
            {
                foreach (var item in anagrams)
                {
                    CachedEntity cachedEntity = new CachedEntity()
                    {
                        RequestId = requestId,
                        AnagramId = item.Id
                    };
                    _cacheRepository.Add(cachedEntity);
                }
            }
        }
    }
}
