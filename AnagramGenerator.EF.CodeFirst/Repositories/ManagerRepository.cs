using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly CFDB_AnagramSolverContext _db;
        public ManagerRepository(CFDB_AnagramSolverContext db)
        {
            _db = db;
        }
        public void TruncateTable(string tableName)
        {
            _db.Database.ExecuteSqlCommand("TruncateTable @TABLENAME", new SqlParameter("@TABLENAME", tableName));
        }

        public IList<string> LoadWords(int page)
        {
            var loadedWords =_db.Words
                                    .Skip((page - 1) * 100)
                                    .Take(100)
                                    .OrderBy(p => p.Word)
                                    .Select(p => p.Word)
                                    .ToList();

            return loadedWords;
        }

    }
}
