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
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=CFDB_AnagramSolver";
        }
        public IList<CacheModel> GetCachedWordsByRequestId(int id)
         {

            IList<CacheModel> anagramsFromCache = new List<CacheModel>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select w.Id FROM Words as w, CachedWords as cw WHERE cw.RequestId = @id AND cw.AnagramID = w.ID", cn);
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;
                SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                    var v = new CacheModel
                    {
                        RequestId = id,
                        AnagramId = (int)reader["w.Id"]
                        };

                        anagramsFromCache.Add(v);
                    }
            }
            return anagramsFromCache;
        }
        public void InsertWordToCache(int requestId, int anagramID)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO CachedWords(SearchedWord, AnagramID) VALUES (@requestId, (SELECT Id FROM Words WHERE Word = @anagram))", cn);
                cmd.Parameters.Add("@requestId", SqlDbType.Int);
                cmd.Parameters.Add("@anagram", SqlDbType.NVarChar);
                cmd.Parameters["@requestId"].Value = requestId;
                cmd.Parameters["@anagram"].Value = anagramID;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public bool WordExists(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
