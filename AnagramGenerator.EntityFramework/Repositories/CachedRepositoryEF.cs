using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.DatabaseFirst.Models;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class CachedRepositoryEF : ICacheRepository
    {
        AnagramDatabaseContext db;
        public CachedRepositoryEF()
        {
            db = new AnagramDatabaseContext();
        
        }
        public IList<WordModel> CheckCached(string word)
        {
            var q = db.Words
                .Join(db.CachedWords,
                w => w.Id,
                cw => cw.AnagramId,
                (w, cw) => new { wm = w, cm = cw })
                .Where(p => p.cm.SearchedWord == word)
                .Select(a => new WordModel() { Word = a.wm.Word, SortedWord = a.wm.SortedWord, Id = a.wm.Id}).ToList();

            return q;
        }

        public void InsertWordToCache(string word, IList<WordModel> anagrams)
        {
            foreach(var item in anagrams)
            {
                CachedWords cachedWords = new CachedWords()
                {
                    AnagramId = item.Id,
                    SearchedWord = word,
                };
                db.CachedWords.Add(cachedWords);
                db.SaveChanges();
             }
        }

    }
}
