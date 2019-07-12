using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.DataAccess
{
    public class CacheRepository : ICacheRepository
    {
        private string connectionString;
        public CacheRepository()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase";
        }
        public IList<WordModel> CheckCached(string word)
         {

            IList<WordModel> anagramsFromCache = new List<WordModel>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select w.Word, w.Id, w.SortedWord FROM Words as w, CachedWords as cw WHERE cw.SearchedWord = @word AND cw.AnagramID = w.ID", cn);
                cmd.Parameters.Add("@word", SqlDbType.NVarChar);
                cmd.Parameters["@word"].Value = word;
                SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var v = new WordModel
                        {
                            Id = (int)reader["Id"],
                            Word = (string)reader["Word"],
                            SortedWord = (string)reader["SortedWord"]
                        };

                        anagramsFromCache.Add(v);
                    }
            }
            return anagramsFromCache;
        }
        public void InsertWordToCache(string phrase, IList<WordModel> anagrams)
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
                    cmd.Parameters["@anagram"].Value = item.Word;
                    cmd.ExecuteNonQuery();
                }
                cn.Close();
            }
        }
    }
}
