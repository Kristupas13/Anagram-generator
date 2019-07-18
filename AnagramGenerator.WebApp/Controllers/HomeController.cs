using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using AnagramGenerator.WebApp.Services;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly WordService _wordService;
        private readonly UserService _userService;
        private readonly CacheService _cacheService;
        private readonly RequestService _requestService;
        private readonly string connectionString;
          
        public HomeController(WordService wordService, UserService userService,CacheService cacheService, RequestService requestService, IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("connectionString");

            _wordService = wordService;
            _userService = userService;
            _cacheService = cacheService;
            _requestService = requestService;
        }
        public IActionResult Index(string phrase = "")
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            AnagramList anagramModel = new AnagramList();
            IList<WordModel> wordModels = new List<WordModel>();
            ViewBag.Message = "";

           // _userService.

            if (!string.IsNullOrWhiteSpace(phrase))
            {

                bool access = _userService.CheckIPLimit(ipAddress);

                if (access)
                {
                    RequestModel request = _requestService.RequestedWord(phrase);

                    IList<CacheModel> cachedWords = _cacheService.GetCachedByRequestId(request.Id);
                 
                    wordModels = cachedWords.Any() ? _requestService.ConvertCacheModelToWordModel(cachedWords) : _requestService.FindAnagrams(request.Word);


                    _cacheService.InsertWordToCache(request.Id, wordModels);

                    _userService.InsertToUserLog(request.Id, ipAddress);
                }
                else
                {
                    ViewBag.Message = "Number of requests exceeded. Insert new word to gain access";
                }
            }

            anagramModel.Anagrams = wordModels;

            return View(anagramModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
   
}
