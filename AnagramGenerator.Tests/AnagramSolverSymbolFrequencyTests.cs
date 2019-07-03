using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;

namespace AnagramSolverSymbolFrequency.Tests
{
    [TestFixture]
    class AnagramSolverSymbolFrequencyTests
    {
        private string Word; //
        public AnagramSolver solver { get; set; }

        [SetUp]
        public void Setup()
        {
            IFileRepository textFileLoader = new TextFileRepository(@"C:\Users\kristupas\Desktop\DataManipulation\MainApp\zodynas.txt");      // testing
            IWordRepository wordRepository = new WordRepository(textFileLoader);
            solver = new AnagramSolver(wordRepository);
        }

        [Test]
        public void AnagramSolverSymbolFrequencyShouldReturnSingleWordCorrectValue()
        {
            // arrange
            Word = "labas";
            var expectedDictionary = new Dictionary<char, int>
            {
            {'a', 2},
            {'l', 1},
            {'b', 1},
            {'s', 1}
            };

            // act
            var actualDictionary = solver.SymbolFrequency(Word);

            // assert
            CollectionAssert.AreEquivalent(expectedDictionary, actualDictionary);
        }
        [Test]
        public void AnagramSolverSymbolFrequencyShouldReturnZeroIfNothingIsPassed()
        {
            // arrange
            Word = "";

            // act
            var actualDictionary = solver.SymbolFrequency(Word);

            // assert
            CollectionAssert.IsEmpty(actualDictionary);
        }
        [Test]
        public void AnagramSolverSymbolFrequencyShoulReturnWordCombinationCorrectValue()
        {
            // arrange
            Word = "labas petras";
            var expectedDictionary = new Dictionary<char, int>
            {
            {'a', 3},
            {'l', 1},
            {'b', 1},
            {'s', 2},
            {'p', 1},
            {'t', 1},
            {'r', 1},
            {'e', 1}
            };

            // act
            var actualDictionary = solver.SymbolFrequency(Word);

            // assert
            CollectionAssert.AreEquivalent(expectedDictionary, actualDictionary);
        }
    }
}
