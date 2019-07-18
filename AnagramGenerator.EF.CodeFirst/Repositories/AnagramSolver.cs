using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class AnagramSolver : IAnagramSolver
    {
        CFDB_AnagramSolverContext db;
        public AnagramSolver()
        {
            db = new CFDB_AnagramSolverContext();
        }
        public IList<WordModel> GetAnagramsSeperated(string myWord)
        {
         var q = db.Words
        .Where(p => p.SortedWord == myWord)
        .Select(p => new WordModel() { Id = p.Id, SortedWord = p.SortedWord, Word = p.Word }).ToList();
            return q;
        }
    }
}
