using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class SQLWordRepository : IWordRepository
    {
        readonly string connectionString;
        public SQLWordRepository()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase";
        }
        public List<string> FindByWordPart(string wordPart)
        {
            List<string> wordsByPart = new List<string>();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                  
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Word FROM Words WHERE Word LIKE '%' + @wordPart + '%'", cn);
                    cmd.Parameters.Add("@wordPart", SqlDbType.NVarChar);
                    cmd.Parameters["@wordPart"].Value = wordPart;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        wordsByPart.Add((string)reader["Word"]);
                    }

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Your file could not be found!");
                Console.WriteLine(e.Message);
            }

            return wordsByPart;
        }
        public IList<string> GetAnagrams(string sortedWord)
        {
            List<string> anagrams = new List<string>();
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select Word FROM Words WHERE SortedWord = @sortedWord ", cn);
                cmd.Parameters.Add("@sortedWord", SqlDbType.NVarChar);
                cmd.Parameters["@sortedWord"].Value = sortedWord;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    anagrams.Add(((string)reader["Word"]));
                }

            }
            return anagrams;
        }

        public IList<string> LoadWords(int page)
        {
            List<string> words = new List<string>();
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select Word FROM Words ORDER BY Word OFFSET @amount ROWS FETCH NEXT 100 ROWS ONLY ", cn);
                cmd.Parameters.Add("@amount", SqlDbType.Int);
                cmd.Parameters["@amount"].Value = (page-1)*100;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    words.Add(((string)reader["Word"]));
                }

            }
            return words;
        }


        public Dictionary<string, HashSet<string>> Load()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, HashSet<string>> GetDictionary()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllWords()
        {
            throw new NotImplementedException();
        }
        
    }
}
