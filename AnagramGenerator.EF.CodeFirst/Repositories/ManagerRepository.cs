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



    }
}
