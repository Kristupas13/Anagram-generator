using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramGenerator.EF.CodeFirst.Interfaces
{
    public class TextRepository : ITextRepository
    {
       private readonly CFDB_AnagramSolverContext _db;

        public TextRepository(CFDB_AnagramSolverContext db)
        {
            _db = db;
        }
        public List<string> Find(string wordPart)
        {
            var q = _db.Words
                .Where(p => p.Word.Contains(wordPart))
                .Select(p => p.Word)
                .ToList();

            return q;
        }

        public IList<WordModel> GetAnagrams(string sortedWord)
        {
            var q = _db.Words
                .Where(p => p.SortedWord == sortedWord)
                .Select(p => new WordModel() { Id = p.Id, SortedWord = p.SortedWord, Word = p.Word }).ToList();

            return q;
        }
        public IList<string> LoadWords(int page)
        {
            var q = _db.Words
                .Skip((page - 1) * 100)
                .Take(100)
                .OrderBy(p => p.Word)
                .Select(p => p.Word)
                .ToList();

            return q;
        }

        public List<string> GetWords()
        {
            var q = _db.Words
                .Select(p => p.Word)
                .ToList();

            return q;
        }

        public void Add(string word)
        {
            WordEntity wordEntity = new WordEntity()
            {
                Word = word,
                SortedWord = string.Concat(word.ToLower().OrderBy(c => c))
            };

            _db.Words.Add(wordEntity);
            _db.SaveChanges();
        }
        public void Remove(WordModel word)
        {
            WordEntity wordEntity = new WordEntity()
            {
                Id = word.Id,
                Word = word.Word,
                SortedWord = word.SortedWord
            };
            _db.Words.Remove(wordEntity);
            _db.SaveChanges();
        }
        public void Edit(WordModel word, string newWord)
        {
            WordEntity newWordEntity = new WordEntity()
            {
                Id = word.Id,
                Word = newWord,
                SortedWord = string.Concat(newWord.ToLower().OrderBy(x => x))
            };


            _db.Attach(newWordEntity);
            _db.Entry(newWordEntity).Property(x => x.Word).IsModified = true;
            _db.Entry(newWordEntity).Property(x => x.SortedWord).IsModified = true;
            _db.SaveChanges();

        }

        public Dictionary<string, HashSet<string>> GetDictionary()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, HashSet<string>> Load()
        {
            throw new NotImplementedException();
        }

        public void Add(WordEntity wordEntity)
        {
            throw new NotImplementedException();
        }

        public void Remove(WordEntity wordEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(WordEntity word)
        {
            throw new NotImplementedException();
        }
    }
}
