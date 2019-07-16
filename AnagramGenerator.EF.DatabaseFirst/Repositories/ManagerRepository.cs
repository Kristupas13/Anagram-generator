using AnagramGenerator.Contracts;
using AnagramGenerator.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    class ManagerRepository : IManagerRepository
    {
        readonly AnagramDatabaseContext db;
        public ManagerRepository()
        {
            db = new AnagramDatabaseContext();
        }
        public void TruncateTable(string tableName)
        {
            db.Database.ExecuteSqlCommand("TruncateTable @TABLENAME", new SqlParameter("@TABLENAME", tableName));
        }

        public bool WordExists(string word)
        {
            var q = db.Words.Where(p => p.Word == word).Any();
            return q;
        }
    }
}
