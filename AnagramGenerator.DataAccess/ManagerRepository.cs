using AnagramGenerator.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramGenerator.DataAccess
{
    public class ManagerRepository : IManagerRepository
    {
        private string connectionString;
        public ManagerRepository()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=CFDB_AnagramSolver";
        }

        public void NewUser(string ip)
        {
            throw new NotImplementedException();
        }

        public void TruncateTable(string tableName)
        {
            string spName = @"dbo.[DeleteTable]";
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(spName, cn);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = @"TableName";
                param1.SqlDbType = SqlDbType.NVarChar;
                param1.Value = tableName;
                cmd.Parameters.Add(param1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public bool WordExists(string word)
        {
            throw new NotImplementedException();
        }
    }
}
