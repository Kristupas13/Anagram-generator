using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.EF.DatabaseFirst.Models;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class CacheWordsRepositoryEF : ICacheRepository
    {
        AnagramDatabaseContext em;
        public CacheWordsRepositoryEF()
        {
            em = new AnagramDatabaseContext();
        }
        public IList<WordModel> CheckCached(WordModel word)
        {
            throw new NotImplementedException();
        }
/*        SqlCommand cmd = new SqlCommand("Select w.Word, w.Id FROM Words as w, CachedWords as cw WHERE cw.SearchedWord = @word AND cw.AnagramID = w.ID", cn);*/

        public void InsertWordToCache(WordModel word, IList<WordModel> anagrams)
        {
            throw new NotImplementedException();
        }
    }
}
