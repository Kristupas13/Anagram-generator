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
            //IFileRepository textFileLoader = new TextFileRepository();
            IWordRepository wordRepository = new TextWordRepository();
            IAnagramSolver anagramSolver = new AnagramSolver(wordRepository);
            Dictionary<string, HashSet<string>> allWords = wordRepository.GetDictionary();
            HashSet<string> wordsToDatabase = new HashSet<string>();

            foreach (var key in allWords.Keys)
                foreach (var item in allWords[key])
                    wordsToDatabase.Add(item);

            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase";

      //      ManageDataBase.DeleteAll(connectionString);
        //    ManageDataBase.Insert(wordsToDatabase, connectionString);
          
        }
    }
}
