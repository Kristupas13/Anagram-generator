using AnagramGenerator.Contracts;
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
        CFDB_Context db;
        public ManagerRepository()
        {
            db = new CFDB_Context();
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
