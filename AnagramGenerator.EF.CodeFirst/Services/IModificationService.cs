using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Services
{
    public interface IModificationService
    {
        bool AddWord(string word);

        bool RemoveWord(string word);

        bool EditWord(string oldWord, string newWord);

        void TruncateTable(string tableName);
    }
}
