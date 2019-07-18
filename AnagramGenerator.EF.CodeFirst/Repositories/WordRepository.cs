using System.Linq;
using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class WordRepository : IWordRepository
    {
        readonly CFDB_AnagramSolverContext db;
        public WordRepository()
        {
            db = new CFDB_AnagramSolverContext();
            
        }

        public IList<WordEntity> GetAll()
        {
            return db.Words.ToList();
        }

        public WordEntity Get(int wordId)
        {
            return db.Words.Find(wordId);
        }

        public int Add(WordEntity wordEntity)
        {
            throw new System.NotImplementedException();
        }

        public WordEntity Update(WordEntity wordEntity)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(WordEntity requestEntity)
        {
            throw new System.NotImplementedException();
        }






        public WordModel ToWordModel(string phrase)
        {
            var q = db.Words.Where(x => x.Word == phrase).Select(x => new WordModel() { Id = x.Id, SortedWord = x.SortedWord, Word = x.Word }).FirstOrDefault();
/*
            db.Database.ExecuteSqlCommand("TruncateTable @TABLENAME", new SqlParameter("@TABLENAME", "UserLogs"));*/
            return q;
        }
        public WordModel GetWordModel(int ID)
        {
            var q = db.Words.Where(x => x.Id == ID).Select(x => new WordModel() { Id = x.Id, SortedWord = x.SortedWord, Word = x.Word }).FirstOrDefault();

            return q;
        }
        public int GetWordID(string word)
        {
            int wordID = db.Words.Where(x => x.Word == word).Select(x => x.Id).FirstOrDefault();


            return wordID;
        }
        public bool WordExists(string word)
        {
            var q = db.Words.Where(p => p.Word == word).Any();

            return q;
        }

    }
}
