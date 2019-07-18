using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IWordRepository
    {
        WordModel ToWordModel(string phrase);
        WordModel GetWordModel(int ID);
        int GetWordID(string word);
    }
}
