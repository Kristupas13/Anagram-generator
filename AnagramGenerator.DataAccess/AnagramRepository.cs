using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramGenerator.DataAccess
{
    public class AnagramRepository : IAnagramRepository
    {
        string connectionString;
        public AnagramRepository()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase";
        }
        public WordModel GetWordModel(string phrase)
        {
            WordModel wm = new WordModel();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select ID, Word, SortedWord FROM Words WHERE Word = @word", cn);
                cmd.Parameters.Add("@word", SqlDbType.NVarChar);
                    cmd.Parameters["@word"].Value = phrase;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                         wm.Id = (int)reader["Id"];
                         wm.Word = (string)reader["Word"];
                         wm.SortedWord = (string)reader["SortedWord"];
                    }
                    cn.Close();
            }
            return wm;
         }
        private IList<string> SeperateWords(string phrase)
        {
            var seperatedWords = phrase.ToLower().Split(" ");
            return seperatedWords;
        }

    }
}
