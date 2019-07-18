using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.DatabaseFirst.Models;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class WordRepository : IWordRepository
    {
        readonly AnagramDatabaseContext db;
        public WordRepository()
        {
            db = new AnagramDatabaseContext();
        }
        public WordModel ToWordModel(string phrase)
        {
            var q = db.Words.Where(x => x.Word == phrase).Select(x => new WordModel() { Id = x.Id, SortedWord = x.SortedWord, Word = x.Word }).FirstOrDefault();

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
            throw new NotImplementedException();
        }
    }
}
