using NUnit.Framework;
using System.Collections.Generic;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using System.Linq;

namespace AnagramSolverSeperated.Tests
{

    [TestFixture]
    public class AnagramSolverSeperatedTests
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
        public void AnagramSolverSimpleWordShouldNotHaveItself()
        {
            // arrange
            Word = "storas";

            // act
            var actualList = solver.GetAnagramsSeperated(Word);

            //assert
            CollectionAssert.DoesNotContain(actualList, Word);
        }

        [Test]
        public void AnagramSolverSimpleShouldHaveReturnFirstAnagramFound()
        {
            // arrange
            Word = "storas";
            List<string> expectedList = new List<string>();
            expectedList.AddRange(new string[] { "trasos"});

            // act
            var actualList = solver.GetAnagramsSeperated(Word);

            // assert
            CollectionAssert.AreEquivalent(expectedList, actualList);
        }

        [Test]
        public void AnagramSolverSimpleShouldReturnWordCombination()
        {
            // arrange
            Word = "storas dievas";
            List<string> expectedList = new List<string>();
            expectedList.AddRange(new string[] { "trasos", "veidas"});

            // act
            var actualList = solver.GetAnagramsSeperated(Word);

            // assert
            CollectionAssert.AreEquivalent(expectedList, actualList);
        }

        [Test]
        public void AnagramSolverSimpleShouldReturnZeroIfNoAnagramsFound()
        {
            // arrange
            Word = "ožka";

            // act
            var actualList = solver.GetAnagramsSeperated(Word);

            // assert
            CollectionAssert.IsEmpty(actualList);
        }
        [Test]
        public void AnagramSolverSimpleShouldReturnZeroIfNothingIsPassed()
        {
            // arrange
            Word = "";

            // act
            var actualList = solver.GetAnagramsSeperated(Word);

            // assert
            CollectionAssert.IsEmpty(actualList);
        }

        [Test]
        public void AnagramSolverSimpleShouldReturnZeroIfNotAllWordsHaveAnagrams()
        {
            // arrange
            Word = "storas mop";

            // act
            var actualList = solver.GetAnagramsSeperated(Word);

            // assert
            CollectionAssert.IsEmpty(actualList);
        }

    }

 }