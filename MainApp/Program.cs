using System;
using System.IO;
using Interfaces.AnagramSolver;
using Implementation.AnagramSolver;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type your phrase: ");
            string phrase = Console.ReadLine();


            IFileRepository loader = new TextFileRepository(@"C:\Users\kristupas\Desktop\DataManipulation\MainApp\zodynas.txt");      // testing
            IWordRepository repository = new WordRepository(loader);
            IAnagramSolver solver = new AnagramSolver(repository);
            IList<string> anagrams = solver.GetAnagramsSimple(phrase);   // works for seperate words

            foreach(var item in anagrams)
            {
               Console.WriteLine("Anagram: {0}", item);
            }
          
        }

    }
}
