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
        private readonly CFDB_AnagramSolverContext _db;
        public AnagramSolver(CFDB_AnagramSolverContext db)
        {
            _db = db;
        }
        public IList<string> GetAnagramsSeperated(string myWord)
        {
            string sortedInputWord = string.Concat(myWord.ToLower().OrderBy(x => x));
         var q = _db.Words
        .Where(p => p.SortedWord == sortedInputWord)
        .Select(p => p.Word).ToList();
            return q;
        }
    }
}
