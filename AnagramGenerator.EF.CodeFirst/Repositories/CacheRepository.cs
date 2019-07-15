using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        readonly Solver_DBContext db;
        public CacheRepository()
        {
            db = new Solver_DBContext();

        }
        public IList<CacheModel> CheckCached(string word)
        {
            var q = db.Words
                .Join(db.CachedWords,
                w => w.Id,
                cw => cw.AnagramId,
                (w, cw) => new { wm = w, cm = cw })
                .Where(p => p.cm.SearchedWord == word)
                .Select(a => new CacheModel() { Id = a.cm.Id, SearchedWord = word, AnagramId = a.wm.Id }).ToList();

            return q;
        }

        public void InsertWordToCache(string word, IList<int> anagrams)
        {
            foreach (var item in anagrams)
            {
                CachedEntity cachedWords = new CachedEntity()
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
