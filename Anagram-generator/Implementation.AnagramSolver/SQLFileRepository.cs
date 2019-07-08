using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class SQLFileRepository : IFileRepository
    {
        Dictionary<string, HashSet<string>> words = new Dictionary<string, HashSet<string>>();
        string connectionString;
        public SQLFileRepository()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase"; 
        }
        public Dictionary<string, HashSet<string>> Load()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Select * FROM Words", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        var languagePart = (string)reader["Part"];
                        var word = (string)reader["Word"];
                        if(words.ContainsKey(languagePart))
                        {
                            words[languagePart].Add(word);
                        }
                        else
                        {
                            words[languagePart] = new HashSet<string> { word };
                        }

                    }
                   
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Your file could not be found!");
                Console.WriteLine(e.Message);
            }

            return words;
        }
        public List<string> FindByWordPart()
        {
            throw new System.NotImplementedException();
        }
    }
}
