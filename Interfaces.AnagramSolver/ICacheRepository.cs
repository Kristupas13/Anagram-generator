using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface ICacheRepository
    {
        IList<string> CheckCached(string word);
        void InsertWordToCache(string word, IList<string> anagrams);
    }
}
