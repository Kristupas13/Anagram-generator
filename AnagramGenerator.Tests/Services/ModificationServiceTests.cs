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
using System.Linq;
using System.Text;

namespace AnagramGenerator.Tests.Services
{
    [TestFixture]
    public class ModificationServiceTests
    {
        private IWordRepository _wordRepository;
        private IManagerRepository _managerRepository;
        private IModificationService _modificationService;
        private string RequestedWord { get; set; }

        [SetUp]
        public void SetUp()
        {
            _wordRepository = Substitute.For<IWordRepository>();
            _managerRepository = Substitute.For<IManagerRepository>();

            _modificationService = new ModificationService(_managerRepository, _wordRepository);
            
        }
        [Test]
        public void AddWord_ShouldAddWordToDictionaryAndReturnTrue()
        {
            RequestedWord = "labas";

            _wordRepository.GetByWord(RequestedWord).ReturnsNull();

            var result = _modificationService.AddWord(RequestedWord);


            _wordRepository.Received().GetByWord(RequestedWord);
            result.ShouldBeTrue();
        }

        [Test]
        public void AddWord_ShouldNotAddExistingDictionaryWord()
        {
            RequestedWord = "labas";

            WordEntity wordEntity = new WordEntity()
            {
                Id = 1,
                Word = "labas",
                SortedWord = "aabls"
            };

            _wordRepository.GetByWord(RequestedWord).Returns(wordEntity);

             var result = _modificationService.AddWord(RequestedWord);

            _wordRepository.Received().GetByWord(RequestedWord);
            _wordRepository.DidNotReceive().Add(wordEntity);
            result.ShouldBeFalse();

        }
        [Test]
        public void RemoveWord_ShouldRemoveExistingWordAndReturnTrue()
        {
            RequestedWord = "labas";

            WordEntity wordEntity = new WordEntity()
            {
                Id = 1,
                Word = "labas",
                SortedWord = "aabls"
            };
            _wordRepository.GetByWord(RequestedWord).Returns(wordEntity);



            var result = _modificationService.RemoveWord(RequestedWord);

            result.ShouldBeTrue();
            _wordRepository.Received().GetByWord(RequestedWord);
            _wordRepository.Received().Remove(wordEntity);


        }
        [Test]
        public void RemoveWord_ShouldReturnFalseIfWordDoesNotExists()
        {
            RequestedWord = "labas";

            WordEntity wordEntity = new WordEntity()
            {
                Id = 1,
                Word = "labas",
                SortedWord = "aabls"
            };
            _wordRepository.GetByWord(RequestedWord).ReturnsNull();



            var result = _modificationService.RemoveWord(RequestedWord);

            result.ShouldBeFalse();
            _wordRepository.Received().GetByWord(RequestedWord);
            _wordRepository.DidNotReceive().Remove(wordEntity);



        }
        [Test]
        public void EditWord_ShouldEditExistingWordAndReturnTrue()
        {
            string newWord = "lab";
            RequestedWord = "labas";

            WordEntity oldWordEntity = new WordEntity()
            {
                Id = 1,
                Word = "labas",
                SortedWord = "aabls"
            };


            _wordRepository.GetByWord(RequestedWord).Returns(oldWordEntity);



            var result = _modificationService.EditWord(oldWordEntity.Word, newWord);

            _wordRepository.Received().GetByWord(RequestedWord);
            _wordRepository.Received().Update(oldWordEntity);
            result.ShouldBeTrue();


        }
        [Test]
        public void EditWord_ShouldReturnFalseIfWordToEditDoesNotExists()
        {
            RequestedWord = "labas";
            string newWord = "lab";
            WordEntity oldWordEntity = new WordEntity()
            {
                Id = 1,
                Word = "labas",
                SortedWord = "aabls"
            };


            _wordRepository.GetByWord(RequestedWord).ReturnsNull();



            var result = _modificationService.EditWord(oldWordEntity.Word, newWord);

            _wordRepository.Received().GetByWord(RequestedWord);
            _wordRepository.DidNotReceive().Update(oldWordEntity);
            result.ShouldBeFalse();


        }
    }
}
