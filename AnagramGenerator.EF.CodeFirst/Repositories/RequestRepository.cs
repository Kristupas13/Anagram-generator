using AnagramGenerator.Contracts;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        CFDB_AnagramSolverContext db;
        public RequestRepository()
        {
            db = new CFDB_AnagramSolverContext();
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
