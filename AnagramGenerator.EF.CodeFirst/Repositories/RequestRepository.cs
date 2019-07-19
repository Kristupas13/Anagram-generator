using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class RequestRepository : IRequestRepository // IBaseRepository<T> : where T is Entity
    {
        private readonly CFDB_AnagramSolverContext _db;
        public RequestRepository(CFDB_AnagramSolverContext db)
        {
            _db = db;
        }

        public IList<RequestEntity> GetAll()
        {
            return _db.RequestWords.ToList();
        }

        public RequestEntity Get(int requestId)
        {
            return _db.RequestWords.Find(requestId);
        }

        public int Add(RequestEntity requestEntity)
        {
            _db.RequestWords.Add(requestEntity);
            _db.SaveChanges();
            return requestEntity.Id;
        }

        public RequestEntity Update(RequestEntity requestEntity)
        {
            throw new NotImplementedException();
        }


        public bool Contains(RequestEntity requestEntity)
        {
            return _db.RequestWords.Contains(requestEntity);
        }

        public RequestEntity GetByWord(string requestWord)
        {
            return _db.RequestWords.SingleOrDefault(p => p.Word == requestWord);
        }
    }
}
