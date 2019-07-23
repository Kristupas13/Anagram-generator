using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.WebApp.Controllers.api;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Tests.Controllers
{
    [TestFixture]
    public class apiGetAnagramsTests
    {
        private GetAnagramsController api;
        private string RequestedWord { get; set; }
        private IRequestService _requestService;
        [SetUp]
        public void SetUp()
        {
            _requestService = Substitute.For<IRequestService>();
            api = new GetAnagramsController(_requestService);
        }

        [Test]
        public void apiController_ShouldReturnWordAnagrams()
        {
            RequestedWord = "labas";

            IList<string> anagrams = new List<string>() { "labas", "balas" };

            _requestService.DetectAnagrams(RequestedWord).Returns(anagrams);

            var result = api.Get(RequestedWord);

            result.ShouldNotBeEmpty();
            result.ShouldBe(anagrams);

            _requestService.Received().DetectAnagrams(RequestedWord);

        }
    }
}
