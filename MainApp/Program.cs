using System;
using System.IO;
using Interfaces.AnagramSolver;
using Implementation.AnagramSolver;
using System.Collections.Generic;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type your phrase: ");
            string phrase = Console.ReadLine();


            LoadRespository loader = new TextLoadRespository(@"C:\Users\kristupas\Desktop\DataManipulation\MainApp\zodynasLite.txt");      // testing
            IWordRepository repository = new WordRepository(loader);
            IAnagramSolver solver = new AnagramSolver(repository, phrase);  
          
        }
        
    }
}
