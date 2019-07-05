using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Tests
{
    class AnagramSolverTests
    {
        private string Word; //
        private int MinLength;
        public AnagramSolver Solver { get; set; }


        [SetUp]
        public void Setup()
        {
            IFileRepository textFileLoader = new TextFileRepository(@"C:\Users\kristupas\Desktop\DataManipulation\MainApp\zodynas.txt");      // testing
            IWordRepository wordRepository = new WordRepository(textFileLoader);
            Solver = new AnagramSolver(wordRepository);
        }

        [Test]
        public void AnagramSolverShouldNotContainItself()
        {
            // arrange
            Word = "storas";

            // act
            var actualList = Solver.GetAnagrams(Word);

            //assert
            CollectionAssert.DoesNotContain(actualList, Word);
        }

        [Test]
        public void AnagramSolverShouldReturnNothing()
        {
            // arrange
            Word = "";

            // act
            var actualList = Solver.GetAnagrams(Word);

            //assert
            CollectionAssert.IsEmpty(actualList);
        }
    }
}
