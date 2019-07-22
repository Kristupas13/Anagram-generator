using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Services
{
    public interface IRequestService
    {

        IList<string> DetectAnagrams(string requestWord);
        IList<string> LoadWords(int page);
        IList<string> Find(string word);
    }
}
