using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst.Models;
using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.WebApp.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Tests.Services
{
    [TestFixture]
    public class RequestServiceTests
    {
        private  IAnagramSolver _anagramSolver;
        private  IWordRepository _wordRepository;
        private  IManagerRepository _managerRepository;
        private  IRequestService _requestService;
        private  string RequestedWord { get; set; }
        private List<string> ExpectedList;

        [SetUp]
        public void SetUp()
        {
            _anagramSolver = Substitute.For<IAnagramSolver>();
            _wordRepository = Substitute.For<IWordRepository>();
            _managerRepository = Substitute.For<IManagerRepository>();

            ExpectedList = new List<string>();

            _requestService = new RequestService(_anagramSolver, _wordRepository, _managerRepository);
        }

        [Test]
        public void DetectAnagrams_ShouldReturnDetectedAnagrams()
        {
            RequestedWord = "dievas";

            ExpectedList.AddRange(new List<string>() { "dievas", "veidas", "vedasi", "deivas"});

            _anagramSolver.GetAnagramsSeperated(RequestedWord).Returns(ExpectedList);

            //////////////////////////////////
            //////////////////////////////////

            var result = _requestService.DetectAnagrams(RequestedWord);

            result.ShouldNotBeEmpty();
            result.ShouldBe(ExpectedList);

            _anagramSolver.Received().GetAnagramsSeperated(RequestedWord);
        }
        [Test]
        public void DetectAnagrams_ShouldReturnNullIfEmpty()
        {
            RequestedWord = "ožq";

            ExpectedList = new List<string>() { };

            _anagramSolver.GetAnagramsSeperated(RequestedWord).Returns(ExpectedList);

            //////////////////////////////////
            //////////////////////////////////

            var result = _requestService.DetectAnagrams(RequestedWord);

            result.ShouldBeEmpty();
            result.ShouldBe(ExpectedList);

            _anagramSolver.Received().GetAnagramsSeperated(RequestedWord);
        }
        [Test]
        public void DetectAnagrams_ShouldReturnNullIfNothingIsPassed()
        {
            RequestedWord = "";

            ExpectedList = new List<string>() { };

            _anagramSolver.GetAnagramsSeperated(RequestedWord).Returns(ExpectedList);

            //////////////////////////////////
            //////////////////////////////////

            var result = _requestService.DetectAnagrams(RequestedWord);

            result.ShouldBeEmpty();
            result.ShouldBe(ExpectedList);

            _anagramSolver.Received().GetAnagramsSeperated(RequestedWord);
        }
        [Test]
        public void Find_ShouldReturnedFoundWords()
        {
            RequestedWord = "vilkas";

            IList<WordEntity> wordEntities = new List<WordEntity>()
            {
                new WordEntity()
                {
                    Id = 1,
                    Word = "vilkas",
                    SortedWord = "ailksv",
                },

                new WordEntity()
                {
                    Id = 1,
                    Word = "vaišvilkas",
                    SortedWord = "aaiilksšv",
                },
            };

            ExpectedList = new List<string>() { wordEntities[0].Word, wordEntities[1].Word };

            _wordRepository.GetListByPartWord(RequestedWord).Returns(wordEntities);

            //////////////////////////////////
            //////////////////////////////////

            var result = _requestService.Find(RequestedWord);

            result.ShouldNotBeNull();
            result.ShouldBe(ExpectedList);

            _wordRepository.Received().GetListByPartWord(RequestedWord);

        }
        [Test]
        public void Find_ShouldReturnEmptyIfNothingIsFound()
        {
            RequestedWord = "ožxz";

            IList<WordEntity> wordEntities = new List<WordEntity>() { };


            ExpectedList = new List<string>() { };

            _wordRepository.GetListByPartWord(RequestedWord).Returns(wordEntities);

            //////////////////////////////////
            //////////////////////////////////

            var result = _requestService.Find(RequestedWord);

            result.ShouldBeEmpty();
            result.ShouldBe(ExpectedList);

            _wordRepository.Received().GetListByPartWord(RequestedWord);

        }
        [Test]
        public void Find_ShouldReturnEmptyIfNothingIsPassed()
        {
            RequestedWord = "";

            IList<WordEntity> wordEntities = new List<WordEntity>() { };


            ExpectedList = new List<string>() { };

            _wordRepository.GetListByPartWord(RequestedWord).Returns(wordEntities);

            //////////////////////////////////
            //////////////////////////////////

            var result = _requestService.Find(RequestedWord);

            result.ShouldBeEmpty();
            result.ShouldBe(ExpectedList);

            _wordRepository.Received().GetListByPartWord(RequestedWord);

        }

    }
}
