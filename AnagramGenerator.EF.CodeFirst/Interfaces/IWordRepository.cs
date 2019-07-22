using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Interfaces
{
    public interface IWordRepository
    {
        IList<WordEntity> GetAll();
        WordEntity Get(int wordId);
        WordEntity GetByWord(string word);
        IList<WordEntity> GetListByPartWord(string partWord);
        int Add(WordEntity wordEntity);
        WordEntity Update(WordEntity wordEntity);
        void Remove(WordEntity wordEntity);
    }
}
