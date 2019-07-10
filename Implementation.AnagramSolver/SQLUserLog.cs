using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class SQLUserLog : IUserLog
    {
        private readonly string connectionString;
        public string IpAddress { get; set; }
        public DateTime Date { get; set; }
        public string SearchedWord { get; set; }
        public IList<string> Anagrams { get; set; }

        public SQLUserLog()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=AnagramDatabase";
        }

        public void InsertToUserLog(string searchedWord, string IpAddress)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO UserLog(UserIP, SearchedWord, Date) VALUES (@userIP, @searchedWord, @Date)", cn);
                cmd.Parameters.Add("@userIP", SqlDbType.NVarChar);
                cmd.Parameters.Add("@searchedWord", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime);
                cmd.Parameters["@userIP"].Value = IpAddress;
                cmd.Parameters["@searchedWord"].Value = searchedWord;
                cmd.Parameters["@Date"].Value = DateTime.Now;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public IUserLog GetUserLog(string ip)
        {
            throw new NotImplementedException();
        }

    }
}
