﻿using System;
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
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (args.Length < 1)
                throw new ArgumentOutOfRangeException("arguments");
            if (args.Length > 10)
                throw new ArgumentOutOfRangeException("arguments");


            string phrase = string.Join(" ", args);

            IFileRepository textFileLoader = new TextFileRepository(@"C:\Users\kristupas\Desktop\DataManipulation\MainApp\zodynas.txt");      // testing
            IWordRepository wordRepository = new WordRepository(textFileLoader);
            IAnagramSolver anagramSolver = new AnagramSolver(wordRepository);

            IList<string> anagrams = anagramSolver.GetAnagramsSeperated(phrase);



            

            Console.WriteLine("Anagram: ");
            foreach(var item in anagrams)
            {
               Console.Write("{0} ", item);
            }
          
        }

    }
}
