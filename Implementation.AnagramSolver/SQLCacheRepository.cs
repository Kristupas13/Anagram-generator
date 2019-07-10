using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class SQLCacheRepository : ICacheRepository
    {
        string connectionString;
        
        public SQLCacheRepository()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase";
        }

        public IList<string> CheckCached(string word)
        {
            IList<string> anagramsFromCache = new List<string>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select w.Word FROM Words as w, CachedWords as cw WHERE cw.SearchedWord = @word AND cw.AnagramID = w.ID", cn);
                cmd.Parameters.Add("@word", SqlDbType.NVarChar);
                cmd.Parameters["@word"].Value = word;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    anagramsFromCache.Add(((string)reader["Word"]));
                }
            }
            return anagramsFromCache;
        }
        public void InsertWordToCache(string phrase, IList<string> anagrams)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO CachedWords(SearchedWord, AnagramID) VALUES (@searchedWord, (SELECT Id FROM Words WHERE Word = @anagram))", cn);
                cmd.Parameters.Add("@searchedWord", SqlDbType.NVarChar);
                cmd.Parameters.Add("@anagram", SqlDbType.NVarChar);
                cmd.Parameters["@searchedWord"].Value = phrase;
                foreach (var item in anagrams)
                {
                    cmd.Parameters["@anagram"].Value = item;
                    cmd.ExecuteNonQuery();
                }
                cn.Close();
            }
        }
    }
}
