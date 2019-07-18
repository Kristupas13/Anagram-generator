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
        int Add(WordEntity wordEntity);
        WordEntity Update(WordEntity wordEntity);
        bool Contains(WordEntity requestEntity);

        WordModel ToWordModel(string phrase);
        WordModel GetWordModel(int ID);
        int GetWordID(string word);
        bool WordExists(string word);
    }
}
