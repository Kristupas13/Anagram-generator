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
        private readonly WordServices _wordService;
        private readonly string connectionString;
          
        public HomeController(WordServices wordService, IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("connectionString");

            _wordService = wordService;       

        }
        public IActionResult Index(string phrase = "")
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            AnagramList anagramModel = new AnagramList();
            IList<WordModel> wordModels = new List<WordModel>();
            ViewBag.Message = "";

            if (!string.IsNullOrWhiteSpace(phrase))
            {

                bool access = _wordService.CheckIPLimit(ipAddress);

                if (access)
                {
                    IList<CacheModel> cachedWords = _wordService.CheckCached(phrase);
                    _wordService.InsertToUserLog(phrase, ipAddress);
                    wordModels = cachedWords.Any() ? _wordService.ConvertCachedToWords(cachedWords) : _wordService.FindAnagrams(phrase);
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
