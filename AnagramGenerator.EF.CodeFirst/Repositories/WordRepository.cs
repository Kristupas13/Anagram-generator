using System.Linq;
using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly CFDB_AnagramSolverContext _db;
        public WordRepository(CFDB_AnagramSolverContext db)
        {
            _db = db;            
        }

        public IList<WordEntity> GetAll()
        {
            return _db.Words.ToList();
        }

        public WordEntity Get(int wordId)
        {
            return _db.Words.Find(wordId);
        }
        public WordEntity GetByWord(string word)
        {
            return _db.Words.SingleOrDefault(p => p.Word == word);
        }
        public IList<WordEntity> GetListByPartWord(string partWord)
        {
            return _db.Words.Where(p => p.Word.Contains(partWord)).ToList();
        }


        public int Add(WordEntity wordEntity)
        {
           _db.Words.Add(wordEntity);
            _db.SaveChanges();
            return wordEntity.Id;
        }

        public WordEntity Update(WordEntity wordEntity)
        {
            _db.Words.Update(wordEntity);
            _db.SaveChanges();
            return wordEntity;
        }

        public void Remove(WordEntity requestEntity)
        {
            _db.Remove(requestEntity);
        }
    }
}
