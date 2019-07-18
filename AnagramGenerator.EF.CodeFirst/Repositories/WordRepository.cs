using System.Linq;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class WordRepository : IWordRepository
    {
        readonly CFDB_AnagramSolverContext db;
        public WordRepository()
        {
            db = new CFDB_AnagramSolverContext();
            
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
    }
}
