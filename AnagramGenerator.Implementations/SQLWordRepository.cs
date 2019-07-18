using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace AnagramGenerator.Implementations
{
    public class SQLWordRepository : ITextRepository
    {
        readonly string connectionString;
        public SQLWordRepository()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=CFDB";
        }
        public List<string> Find(string wordPart)
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
        public IList<WordModel> GetAnagrams(string sortedWord)
        {
            List<WordModel> anagrams = new List<WordModel>();
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select ID, Word FROM Words WHERE SortedWord = @sortedWord ", cn);
                cmd.Parameters.Add("@sortedWord", SqlDbType.NVarChar);
                cmd.Parameters["@sortedWord"].Value = sortedWord;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WordModel wm = new WordModel()
                    {
                        Id = (int)reader["ID"],
                        Word = (string)reader["Word"]
                    };
                    anagrams.Add(wm);
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
                cmd.Parameters["@amount"].Value = (page - 1) * 100;
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

        public List<string> GetWords()
        {
            throw new NotImplementedException();
        }

        public void Add(string word)
        {
            throw new NotImplementedException();
        }

        public void Remove(WordModel word)
        {
            throw new NotImplementedException();
        }

        public void Edit(WordModel word, string newWord)
        {
            throw new NotImplementedException();
        }
    }
}
