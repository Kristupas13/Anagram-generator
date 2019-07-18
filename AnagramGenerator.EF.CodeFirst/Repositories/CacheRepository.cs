using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.EF.CodeFirst.Interfaces;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        readonly CFDB_AnagramSolverContext db;
        public CacheRepository()
        {
            db = new CFDB_AnagramSolverContext();

        }

        public int Add(CachedEntity cacheModel)
        {
            db.CachedWords.Add(cacheModel);
            db.SaveChanges();
          
            return cacheModel.Id;
        }

        public CachedEntity Get(int cacheId)
        {
            return db.CachedWords.Find(cacheId);
        }

        public IList<CachedEntity> GetAll()
        {
            return db.CachedWords.ToList();
        }

        public CachedEntity Update(CachedEntity cachedEntity)
        {
            throw new NotImplementedException();
        }

        public bool Contains(CachedEntity cachedEntity)
        {
            return db.CachedWords.Contains(cachedEntity);
        }





        public IList<CacheModel> GetCachedWordsByRequestId(int requestId)
        {
            var q = db.CachedWords.Where(p => p.RequestId == requestId).Select(p => new CacheModel { Id = p.Id, RequestId = p.RequestId, AnagramId = (int)p.AnagramId }).ToList();
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

        public bool WordExists(int requestId)
        {
            return db.CachedWords.Any(p => p.RequestId == requestId);
        }

    }
}
