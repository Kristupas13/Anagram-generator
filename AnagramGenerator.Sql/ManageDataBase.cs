using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramGenerator.Sql
{
    class ManageDataBase
    {
        public ManageDataBase()
        {

        }
        public static void Insert(Dictionary<string, HashSet<string>> allWords, string connectionString)
        {
            string insertStmt = "INSERT INTO Words(Word, Part) VALUES (@existingWord, @partOfLanguage)";
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(insertStmt, con))
            {
                cmd.Parameters.Add("@existingWord", SqlDbType.NVarChar);
                cmd.Parameters.Add("@partOfLanguage", SqlDbType.NVarChar);
                con.Open();
                foreach (var contents in allWords.Keys)
                {
                    foreach (var listMember in allWords[contents])
                    {
                        cmd.Parameters["@existingWord"].Value = listMember;
                        cmd.Parameters["@partOfLanguage"].Value = contents;
                    cmd.ExecuteNonQuery();
                    }
                }


                con.Close();
            }
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
    }
}
