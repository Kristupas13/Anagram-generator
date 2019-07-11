using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AnagramGenerator.Contracts;


namespace AnagramGenerator.DataAccess
{
    public class CacheRepository : ICacheRepository
    {
        private string connectionString;
        public CacheRepository()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase";
        }
        public IList<WordModel> CheckCached(WordModel word)
         {

            IList<WordModel> anagramsFromCache = new List<WordModel>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select w.Word, w.Id FROM Words as w, CachedWords as cw WHERE cw.SearchedWord = @word AND cw.AnagramID = w.ID", cn);
                cmd.Parameters.Add("@word", SqlDbType.NVarChar);
                cmd.Parameters["@word"].Value = word.Word;
                SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var v = new WordModel
                        {
                            ID = (int)reader["Id"],
                            Word = (string)reader["Word"]
                        };

                        anagramsFromCache.Add(v);
                    }
            }
            return anagramsFromCache;
        }
        public void InsertWordToCache(WordModel phrase, IList<WordModel> anagrams)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO CachedWords(SearchedWord, AnagramID) VALUES (@searchedWord, (SELECT Id FROM Words WHERE Word = @anagram))", cn);
                cmd.Parameters.Add("@searchedWord", SqlDbType.NVarChar);
                cmd.Parameters.Add("@anagram", SqlDbType.NVarChar);
                cmd.Parameters["@searchedWord"].Value = phrase.Word;
                foreach (var item in anagrams)
                {
                    cmd.Parameters["@anagram"].Value = item.Word;
                    cmd.ExecuteNonQuery();
                }
                cn.Close();
            }
        }
    }
}
