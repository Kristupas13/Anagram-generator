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
        private readonly CFDB_AnagramSolverContext _db;
        public CacheRepository(CFDB_AnagramSolverContext db)
        {
            _db = db;
        }

        public int Add(CachedEntity cacheModel)
        {
            _db.CachedWords.Add(cacheModel);
            _db.SaveChanges();
          
            return cacheModel.Id;
        }

        public CachedEntity Get(int cacheId)
        {
            return _db.CachedWords.Find(cacheId);
        }


        public IList<CachedEntity> GetAll()
        {
            var a = _db.CachedWords.ToList();
            var b = _db.RequestWords.Where(x => x.Id == 4).FirstOrDefault();
            return _db.CachedWords.ToList();
        }

        public CachedEntity Update(CachedEntity cachedEntity)
        {
            throw new NotImplementedException();
        }


    }
}
