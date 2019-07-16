using NUnit.Framework;
using System.Collections.Generic;
using AnagramGenerator.Contracts;
using AnagramGenerator.Implementations;
using System.Linq;
using AnagramGenerator.DataAccess;

namespace AnagramSolverSeperated.Tests.TextFileRepository
{

    [TestFixture]
    public class AnagramSolverSeperatedTests
    {
        private string Word; //
        public IAnagramSolver solver;


        [SetUp]
        public void Setup()
        {
            ITextRepository textFileLoader = new TextWordRepository();
            solver = new AnagramSolver(textFileLoader);
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
        public void AnagramSolverSimpleShouldReturnWordCombination()
        {
            // arrange
            Word = "storas dievas";
            List<string> expectedList = new List<string>()
            {
                "trasos",
                "trosas",
                "storas",
                "rastos",
                "dievas",
                "veidas",
                "deivas",
                "vedasi"
            };

            // act
            var wordModelList = solver.GetAnagramsSeperated(Word);

            List<string> actualList = wordModelList.Select(p => p.Word).ToList();

            // assert
            CollectionAssert.AreEquivalent(expectedList, actualList);
        }

        [Test]
        public void AnagramSolverSimpleShouldWorkWithUpperCase()
        {
            // arrange
            Word = "StoRaS dieVas";
            List<string> expectedList = new List<string>()
            {
                "trasos",
                "trosas",
                "storas",
                "rastos",
                "dievas",
                "veidas",
                "deivas",
                "vedasi"
            };

            // act
            var wordModelList = solver.GetAnagramsSeperated(Word);

            List<string> actualList = wordModelList.Select(p => p.Word).ToList();

            // assert
            CollectionAssert.AreEquivalent(expectedList, actualList);
        }


        [Test]
        public void AnagramSolverSimpleShouldReturnZeroIfNoAnagramsFound()
        {
            // arrange
            Word = "opk";

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
        public void AnagramSolverSimpleShouldReturnSingleWordAnagrams()
        {
            // arrange
            Word = "storas mop";
            List<string> expectedList = new List<string>()
            {
                "storas",
                "trasos",
                "trosas",
                "rastos"
            };


            // act
            var wordLists = solver.GetAnagramsSeperated(Word);

            List<string> actualList = wordLists.Select(p => p.Word).ToList();

            // assert
            CollectionAssert.AreEquivalent(expectedList, actualList);
        }

    }

 }