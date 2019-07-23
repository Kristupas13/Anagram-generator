using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst.Models;
using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.WebApp.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;


namespace AnagramGenerator.Tests.Services
{

    [TestFixture]
    public class CacheServiceTests
    {
        private  ICacheRepository _cacheRepository;
        private  IRequestRepository _requestRepository;
        private  IWordRepository _wordRepository;
        private  ICacheService _cacheService;
        private List<string> ExpectedList { get; set; }
        public string RequestedWord { get; set; }

        [SetUp]
        public void SetUp()
        {
            _cacheRepository = Substitute.For<ICacheRepository>();
            _requestRepository = Substitute.For<IRequestRepository>();
            _wordRepository = Substitute.For<IWordRepository>();

            _cacheService = new CacheService(_cacheRepository, _requestRepository, _wordRepository);

        }
        [Test]
        public void GetAnagramsFromCache_ShouldReturnListOfCachedWords()
        {
            RequestedWord = "labas";
            ExpectedList = new List<string>() { "labas", "balas" };

            var firstSample = new CachedEntity()
            {
                Id = 1,
                Anagram = new WordEntity() { Id = 1, Word = "labas", SortedWord = "aabls" },
                Request = new RequestEntity() { Id = 1, Word = "labas" }
            };
            var secondSample = new CachedEntity()
            {
                Id = 1,
                Anagram = new WordEntity() { Id = 2, Word = "balas", SortedWord = "aabls" },
                Request = new RequestEntity() { Id = 1, Word = "labas" }
            };

            IList<CachedEntity> returnedFromCache = new List<CachedEntity>()
            {
                firstSample,                 
                secondSample

            };
            _cacheRepository.GetCacheListByRequestWord(RequestedWord).Returns(returnedFromCache);

            //////////////////////////////////
            //////////////////////////////////

            var result = _cacheService.GetAnagramsFromCache(RequestedWord);

            result.ShouldNotBeNull();

            firstSample.Anagram.Word.ShouldBe(ExpectedList[0]);
            secondSample.Anagram.Word.ShouldBe(ExpectedList[1]);

            _cacheRepository.Received().GetCacheListByRequestWord(RequestedWord);
        }
        [Test]
        public void GetAnagramsFromCache_ShouldReturnEmptyListIfWordHasNoAnagrams()
        {
            RequestedWord = "labas";
            ExpectedList = new List<string>() { };

            _cacheRepository.GetCacheListByRequestWord(RequestedWord).ReturnsNull();

            //////////////////////////////////
            //////////////////////////////////

            var result = _cacheService.GetAnagramsFromCache(RequestedWord);

            result.ShouldBeEmpty();

            _cacheRepository.Received().GetCacheListByRequestWord(RequestedWord);
        }
        [Test]
        public void GetAnagramsFromCache_ShouldReturnEmptyIfNothingIsPassed()
        {
            RequestedWord = "";
            ExpectedList = new List<string>() { };


            var firstSample = new CachedEntity()
            {
               
            };
            var secondSample = new CachedEntity()
            {
                
            };

            IList<CachedEntity> returnedFromCache = new List<CachedEntity>()
            {
                firstSample,
                secondSample
            };

            _cacheRepository.GetCacheListByRequestWord(RequestedWord).Returns(returnedFromCache);

            //////////////////////////////////
            //////////////////////////////////

            var result = _cacheService.GetAnagramsFromCache(RequestedWord);

            result.ShouldBeEmpty();

            _cacheRepository.DidNotReceive().GetCacheListByRequestWord(RequestedWord);
        }
       /* [Test]
        public void InsertWordToRequestedWords_ShouldInsertWordsToRequestedWordsTable()
        {
            RequestedWord = "labas";
            RequestEntity requestEntity = new RequestEntity()
            {
                Id = 1,
                Word = RequestedWord
            };

            _requestRepository.GetByWord(RequestedWord).ReturnsNull();


            _cacheService.InsertWordToRequestedWords(RequestedWord);

            _requestRepository.Received().GetByWord(RequestedWord);
            _requestRepository.Received().Add(requestEntity);
        }*/
    }
}
