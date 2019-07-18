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
        readonly CFDB_AnagramSolverContext db;
        public CacheRepository()
        {
            db = new CFDB_AnagramSolverContext();

        }
        public IList<CacheModel> GetCachedWordsByRequestId(int requestId)
        {
            var q = db.CachedWords.Where(p => p.RequestId == requestId).Select(p => new CacheModel { Id = p.Id, RequestId = p.RequestId, AnagramId = (int)p.AnagramId }).ToList();
            /*      var q = db.Words
                      .Join(db.CachedWords,
                      w => w.Id,
                      cw => cw.AnagramId,
                      (w, cw) => new { wm = w, cm = cw })
                      .Where(p => p.cm.SearchedWord == word)
                      .Select(a => new CacheModel() { Id = a.cm.Id, SearchedWord = word, AnagramId = a.wm.Id }).ToList();*/

            return q;
        }

        public void InsertWordToCache(int requestId, int anagramID)
        {
            CachedEntity cachedWords = new CachedEntity()
            {
                AnagramId = anagramID,
                RequestId = requestId
             };
            db.CachedWords.Add(cachedWords);
            db.SaveChanges();

        }

    }
}
