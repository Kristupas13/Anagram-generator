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
        private readonly IRequestRepository _requestRepository;



        public ModificationService(ITextRepository textRepository, IWordRepository wordRepository, ICacheRepository cacheRepository, IRequestRepository requestRepository) 
        {
            _textRepository = textRepository;
            _wordRepository = wordRepository;
            _cacheRepository = cacheRepository;
            _requestRepository = requestRepository;

        }
        public bool AddWord(string word, string ip)
        {
            bool wordExists = _wordRepository.GetAll().Where(p => p.Word == word).Any();

            if(!wordExists)
            {
                WordEntity wordEntity = new WordEntity()
                {
                    Word = word,
                    SortedWord = string.Concat(word.ToLower().OrderBy(p => p))
                };
                _textRepository.Add(wordEntity);
                return true;
            }           
            return false;
        }
        public bool RemoveWord(string word, string ip)
        {
            bool wordExists = _wordRepository.GetAll().Where(p => p.Word == word).Any();

            if (wordExists)
            {
                int wordId = _wordRepository.GetAll().Where(p => p.Word == word).Select(p => p.Id).Single();

                WordEntity wordEntity = _wordRepository.Get(wordId);

                _textRepository.Remove(wordEntity);

                return true;
            }
            return false;
        }

        public bool EditWord(string oldWord, string newWord, string ip)
        {
            bool wordExists = !_wordRepository.GetAll().Where(p => p.Word == oldWord).Any();
            bool newWordExists = !_wordRepository.GetAll().Where(p => p.Word == newWord).Any();

    /*        if(wordExists && !newWordExists)
            {


                _textRepository.Edit(_wordRepository.ToWordModel(oldWord), newWord);
                return true;
            } */
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
        {/*
            bool wordExists = _wordRepository.WordExists(word);
            return wordExists;*/
            return false;
        }
        public void InsertWordToCache(string requestWord, IList<string> anagrams)
        {
            if (!(_cacheRepository.GetAll().Any(p => p.Request.Word == requestWord)))
            {
                foreach (var item in anagrams)
                {
                    CachedEntity cachedEntity = new CachedEntity()
                    {
                        RequestId = _requestRepository.GetByWord(requestWord).Id,
                        AnagramId = _wordRepository.GetByWord(item).Id,
                    };
                    _cacheRepository.Add(cachedEntity);
                }
            }
        }
    }
}
