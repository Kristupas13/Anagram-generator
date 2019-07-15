using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.EF.CodeFirst
{
    public class TextRepository : ITextRepository
    {
        readonly Solver_DBContext db;

        public TextRepository()
        {
            db = new Solver_DBContext();
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
