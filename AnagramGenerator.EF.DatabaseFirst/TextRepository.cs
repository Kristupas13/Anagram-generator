using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst
{
    public class TextRepository : ITextRepository
    {
        readonly AnagramDatabaseContext db;

        public TextRepository()
        {
            db = new AnagramDatabaseContext();
        }
        public List<string> Find(string wordPart)
        {
            var q = db.Words
                .Where(p => p.Word.Contains(wordPart))
                .Select(p => p.Word)
                .ToList();

            return q;
        }

        public IList<WordModel> GetAnagrams(string sortedWord)
        {
            var q = db.Words
                .Where(p => p.SortedWord == sortedWord)
                .Select(p => new WordModel() { Id = p.Id, SortedWord = p.SortedWord, Word = p.Word }).ToList();

            return q;
        }
        public IList<string> LoadWords(int page)
        {
            var q = db.Words
                .Skip((page - 1) * 100)
                .Take(100)
                .Select(p => p.Word)
                .ToList();

            return q;
        }

        public List<string> GetWords()
        {
            var q = db.Words
                .Select(p => p.Word)
                .ToList();

            return q;
        }


        public Dictionary<string, HashSet<string>> GetDictionary()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, HashSet<string>> Load()
        {
            throw new NotImplementedException();
        }

    }
}
