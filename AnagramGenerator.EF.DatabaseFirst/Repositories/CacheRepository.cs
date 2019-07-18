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
        public IList<CacheModel> GetCachedWordsByRequestId(int word)
        {
            /*            var q = db.Words
                            .Join(db.CachedWords,
                            w => w.Id,
                            cw => cw.AnagramId,
                            (w, cw) => new { wm = w, cm = cw })
                            .Where(p => p.cm. == word)
                            .Select(a => new CacheModel() { Id = a.cm.Id , SearchedWord = word, AnagramId = a.wm.Id }).ToList();*/

            throw new NotImplementedException();
        }

        public void InsertWordToCache(int requestId, int anagramID)
        {
         /*  CachedWords cachedWords = new CachedWords()
              {
                    AnagramId = anagramID,
                    SearchedWord = requestId
           };
              db.CachedWords.Add(cachedWords);
              db.SaveChanges();*/

        }

        public bool WordExists(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
