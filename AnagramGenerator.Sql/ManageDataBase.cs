using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AnagramGenerator.Sql
{
    class ManageDataBase
    {
        public ManageDataBase()
        {

        }

        public static void Insert(HashSet<string> allWords, string connectionString)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string insertStmt = "INSERT INTO Words(Word, SortedWord) VALUES (@existingWord, @sortedWord)";
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(insertStmt, con))
            {
                cmd.Parameters.Add("@existingWord", SqlDbType.NVarChar);
                cmd.Parameters.Add("@sortedWord", SqlDbType.NVarChar);
                con.Open();
                foreach (var contents in allWords)
                {
                    cmd.Parameters["@existingWord"].Value = contents.ToLower();
                    cmd.Parameters["@sortedWord"].Value = String.Concat(contents.ToLower().OrderBy(c => c));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            stopWatch.Stop();
            Console.WriteLine("Time measured: {0} Miliseconds.  ({1} Seconds)", stopWatch.ElapsedMilliseconds, stopWatch.Elapsed.Seconds);
        }

        public static void DeleteAll(string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "TRUNCATE TABLE Words";
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }



        //Insert with language part (linksnis)
        /*  public static void Insert(Dictionary<string, HashSet<string>> allWords, string connectionString)          
          {
              Stopwatch stopWatch = new Stopwatch();
              stopWatch.Start();
              string insertStmt = "INSERT INTO Words(Word, Part, SortedWord) VALUES (@existingWord, @partOfLanguage, @sortedWord)";
              using (SqlConnection con = new SqlConnection(connectionString))
              using (SqlCommand cmd = new SqlCommand(insertStmt, con))
              {
                  cmd.Parameters.Add("@existingWord", SqlDbType.NVarChar);
                  cmd.Parameters.Add("@partOfLanguage", SqlDbType.NVarChar);
                  cmd.Parameters.Add("@sortedWord", SqlDbType.NVarChar);
                  con.Open();
                  foreach (var contents in allWords.Keys)
                  {
                      foreach (var listMember in allWords[contents])
                      {
                          cmd.Parameters["@existingWord"].Value = listMember.ToLower();
                          cmd.Parameters["@partOfLanguage"].Value = contents;
                          cmd.Parameters["@sortedWord"].Value = String.Concat(listMember.ToLower().OrderBy(c => c));
                          cmd.ExecuteNonQuery();
                      }
                  }
                  con.Close();
              }
              stopWatch.Stop();
              Console.WriteLine("Time measured: {0} Miliseconds.  ({1} Seconds)", stopWatch.ElapsedMilliseconds, stopWatch.Elapsed.Seconds);
          }*/
    }
}
