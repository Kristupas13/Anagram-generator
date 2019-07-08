using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AnagramGenerator.Sql
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileRepository textFileLoader = new TextFileRepository();
            IWordRepository wordRepository = new WordRepository(textFileLoader);
            Dictionary<string, HashSet<string>> allWords = wordRepository.GetDictionary();

            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase";

         //  ManageDataBase.Insert(allWords, connectionString);
         //  ManageDataBase.DeleteAll(connectionString);
        }
    }
}
