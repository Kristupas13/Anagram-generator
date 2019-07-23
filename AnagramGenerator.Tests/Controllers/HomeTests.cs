using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.WebApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Tests.Controllers
{
    [TestFixture]
    public class HomeTests
    {
        private IConfiguration configuration;
        private IUserService _userService;
        private IRequestService _requestService;
        private ICacheService _cacheService;

        private HomeController _controller;

        private string RequestedWord { get; set; }

        [SetUp]
        public void SetUp()
        {
            configuration = Substitute.For<IConfiguration>();
            _userService = Substitute.For<IUserService>();
            _requestService = Substitute.For<IRequestService>();
            _cacheService = Substitute.For<ICacheService>();
             

            _controller = new HomeController(_userService, _requestService, _cacheService, configuration);

        }
        [Test]
        public void Index_ShouldReturnAnagrams()
        {
            RequestedWord = "labas";
            string ipAddress = "::1";
            IList<string> anagrams = new List<string>()
            {
                "labas",
                "balas"
            };

            _userService.CheckIPLimit(ipAddress).Returns(true);
            _cacheService.GetAnagramsFromCache(RequestedWord).Returns(anagrams);



            var result = _controller.Index(RequestedWord);

            result.ShouldNotBeNull();
        }

    }
}
