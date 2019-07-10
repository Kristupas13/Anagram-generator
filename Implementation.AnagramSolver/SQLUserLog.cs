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
        public HashSet<string> SearchedWord { get; set; }
        public HashSet<string> Anagrams { get; set; }

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
            IUserLog userLog = new SQLUserLog() { SearchedWord = new HashSet<string>(), Anagrams = new HashSet<string>() };
            using (SqlConnection cn = new SqlConnection(connectionString))
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT UserIp, Date, cw.SearchedWord, Word FROM Words w inner join CachedWords cw on w.Id = cw.AnagramID inner join UserLog u on u.UserIP = @word",
                    cn);
                cmd.Parameters.Add("@word", SqlDbType.NVarChar);
                cmd.Parameters["@word"].Value = ip;

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    userLog.IpAddress = (string)reader["UserIp"];
                    userLog.Date = (DateTime)reader["Date"];
                    userLog.SearchedWord.Add((string)reader["SearchedWord"]);
                    userLog.Anagrams.Add((string)reader["Word"]);
                //    wordsByPart.Add((string)reader["Word"]);
                }
                cn.Close();
            }

            return userLog;
        }

    }
}
