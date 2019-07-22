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
        private readonly IManagerRepository _managerRepository;



        public ModificationService(IManagerRepository managerRepository, IWordRepository wordRepository) 
        {
            _managerRepository = managerRepository;
            _wordRepository = wordRepository;
        }
        public bool AddWord(string word)
        {
            WordEntity wordEntity = _wordRepository.GetByWord(word);

            if (wordEntity != null)
                return false;

            wordEntity = new WordEntity()
            {
                Word = word,
                SortedWord = string.Concat(word.ToLower().OrderBy(x => x))
            };
            _wordRepository.Add(wordEntity);

            return true;

        }
        public bool RemoveWord(string word)
        {
            WordEntity wordEntity = _wordRepository.GetByWord(word);

            if (wordEntity == null)
                return false;


            _wordRepository.Remove(wordEntity);
            return true;
            
        }

        public bool EditWord(string oldWord, string newWord)
        {
            WordEntity oldWordEntity = _wordRepository.GetByWord(oldWord);



            if(oldWordEntity == null)
            {
                return false;
            }

            oldWordEntity.Word = newWord;
            oldWordEntity.SortedWord = string.Concat(newWord.ToLower().OrderBy(x => x));

            _wordRepository.Update(oldWordEntity);

            return true;         
        }

 
        public void TruncateTable(string tableName)
        {
            _managerRepository.TruncateTable(tableName);
        }

    }
}
