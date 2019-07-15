using AnagramGenerator.Contracts;
using AnagramGenerator.Implementations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AnagramGenerator.Sql
{
    class Program
    {
        static void Main(string[] args)
        {
            //IFileRepository textFileLoader = new TextFileRepository();
            ITextRepository wordRepository = new TextWordRepository();
            IAnagramSolver anagramSolver = new AnagramSolver(wordRepository);
            List<string> allWords = wordRepository.GetWords();
            allWords = allWords.Distinct().ToList();

            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=AnagramDatabase";

            ManageDataBase.DeleteAll(connectionString);
            ManageDataBase.Insert(allWords, connectionString);

        }
    }
}
