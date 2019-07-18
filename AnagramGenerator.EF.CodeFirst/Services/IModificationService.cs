using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Services
{
    public interface IModificationService
    {
        bool AddWord(string word, string ip);

        bool RemoveWord(string word, string ip);

        bool EditWord(string oldWord, string newWord, string ip);

        IList<string> Find(string word);

        IList<string> LoadWords(int page);

        bool WordExists(string word);

        void InsertWordToCache(int requestId, IList<WordModel> anagrams);
    }
}
