using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramGenerator.DataAccess
{
    public class UserLogRepository : IUserLogRepository
    {
        private string connectionString;
        public UserLogRepository()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase";
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

        public IList<UserLogModel> GetUserLog(string ip)
        {
            List<UserLogModel> userLogs = new List<UserLogModel>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT UserIp, Date, cw.SearchedWord, Word, w.ID FROM Words w inner join CachedWords cw on w.Id = cw.AnagramID inner join UserLog u on u.UserIP = @word ORDER BY Date",
                    cn);
                cmd.Parameters.Add("@word", SqlDbType.NVarChar);
                cmd.Parameters["@word"].Value = ip;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserLogModel logModel = new UserLogModel()
                    {
                        UserIp = ip,
                        Date = (DateTime)reader["Date"],
                        SearchedWord = (string)reader["SearchedWord"],
                        Id = (int)reader["Id"],
                    };
                    userLogs.Add(logModel);

                }
                cn.Close();
            }

            return userLogs;
        }
    }
}
