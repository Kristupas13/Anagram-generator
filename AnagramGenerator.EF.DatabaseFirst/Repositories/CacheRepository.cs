using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.DatabaseFirst.Models;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        readonly AnagramDatabaseContext db;
        public CacheRepository()
        {
            db = new AnagramDatabaseContext();

        }
        public IList<CacheModel> CheckCached(string word)
        {
            var q = db.Words
                .Join(db.CachedWords,
                w => w.Id,
                cw => cw.AnagramId,
                (w, cw) => new { wm = w, cm = cw })
                .Where(p => p.cm.SearchedWord == word)
                .Select(a => new CacheModel() { Id = a.cm.Id ,SearchedWord = word, AnagramId = a.wm.Id }).ToList();

            return q;
        }

        public void InsertWordToCache(string word, IList<int> anagrams)
        {
            foreach (var item in anagrams)
            {
                CachedWords cachedWords = new CachedWords()
                {
                    AnagramId = item,
                    SearchedWord = word
                };
                db.CachedWords.Add(cachedWords);
                db.SaveChanges();
            }
        }

    }
}
