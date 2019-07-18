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
        CFDB_AnagramSolverContext db;
        public RequestRepository()
        {
            db = new CFDB_AnagramSolverContext();
        }

        public IList<RequestEntity> GetAll()
        {
            return db.RequestWords.ToList();
        }

        public RequestEntity Get(int requestId)
        {
            return db.RequestWords.Find(requestId);
        }

        public int Add(RequestEntity requestEntity)
        {
            db.RequestWords.Add(requestEntity);
            db.SaveChanges();
            return requestEntity.Id;
        }

        public RequestEntity Update(RequestEntity requestEntity)
        {
            throw new NotImplementedException();
        }


        public bool Contains(RequestEntity requestEntity)
        {
            return db.RequestWords.Contains(requestEntity);
        }

        public RequestEntity Get(string requestWord)
        {
            return db.RequestWords.SingleOrDefault(p => p.Word == requestWord);
        }




        public void Add(string word)
        {
            RequestEntity requestEntity = new RequestEntity()
            {
                Word = word
            };
            db.RequestWords.Add(requestEntity);
            db.SaveChanges();
        }

        public int GetID(string word)
        {
            return db.RequestWords.Where(p => p.Word == word).Select(p => p.Id).FirstOrDefault();
        }
        public bool Exists(string word)
        {
            return db.RequestWords.Where(p => p.Word == word).Any();
        }

        public RequestModel ToModel(string word)
        {
            return db.RequestWords.Where(p => p.Word == word).Select(p => new RequestModel() { Id = p.Id, Word = p.Word}).FirstOrDefault();
        }

        public RequestModel ToModel(int id)
        {
            return db.RequestWords.Where(p => p.Id == id).Select(p => new RequestModel() { Id = p.Id, Word = p.Word }).FirstOrDefault();
        }
    }
}
