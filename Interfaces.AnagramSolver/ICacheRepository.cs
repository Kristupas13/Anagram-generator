using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface ICacheRepository
    {
        IList<WordModel> CheckCached(WordModel word);
        void InsertWordToCache(WordModel word, IList<WordModel> anagrams);
    }
}
