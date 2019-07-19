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

        public int Add(WordEntity wordEntity)
        {
            throw new System.NotImplementedException();
        }

        public WordEntity Update(WordEntity wordEntity)
        {
            WordEntity word = _db.Words.Single(p => p.Id == wordEntity.Id);
            word.Word = wordEntity.Word;
            word.SortedWord = wordEntity.SortedWord;
            _db.SaveChanges();
            return word;
        }

        public bool Contains(WordEntity requestEntity)
        {
            throw new System.NotImplementedException();
        }






        public WordModel ToWordModel(string phrase)
        {
            var q = _db.Words.Where(x => x.Word == phrase).Select(x => new WordModel() { Id = x.Id, SortedWord = x.SortedWord, Word = x.Word }).FirstOrDefault();
/*
            db.Database.ExecuteSqlCommand("TruncateTable @TABLENAME", new SqlParameter("@TABLENAME", "UserLogs"));*/
            return q;
        }
        public WordModel GetWordModel(int ID)
        {
            var q = _db.Words.Where(x => x.Id == ID).Select(x => new WordModel() { Id = x.Id, SortedWord = x.SortedWord, Word = x.Word }).FirstOrDefault();

            return q;
        }
        public int GetWordID(string word)
        {
            int wordID = _db.Words.Where(x => x.Word == word).Select(x => x.Id).FirstOrDefault();


            return wordID;
        }
        public bool WordExists(string word)
        {
            var q = _db.Words.Where(p => p.Word == word).Any();

            return q;
        }

    }
}
