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
using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.EF.CodeFirst.Models;

namespace AnagramGenerator.WebApp.Interfaces
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        private readonly IUserService _userService;
        private readonly IRequestService _requestService;
        private readonly ICacheService _cacheService;

        private readonly string connectionString;
          
        public HomeController(IUserService userService, IRequestService requestService, ICacheService cacheService, IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("connectionString");

            _userService = userService;
            _requestService = requestService;
            _cacheService = cacheService;
        }
        public IActionResult Index(string phrase = "")
        {
            AnagramList anagramList = new AnagramList();

            IList<string> anagrams = new List<string>();

            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            ViewBag.Message = "";

            if (!string.IsNullOrWhiteSpace(phrase))
            {
         
                bool access = _userService.CheckIPLimit(ipAddress);

                if (access)
                {

                  
                    anagrams = _cacheService.GetAnagramsFromCache(phrase);

                    if (anagrams.Count() == 0)
                    {
                        anagrams = _requestService.DetectAnagrams(phrase);
                    }

                     _cacheService.InsertWordToCache(phrase, anagrams);
                    
                     _userService.InsertToUserLog(phrase, ipAddress);

                     _userService.DecrementCounter(ipAddress);
                }
                else
                {
                    ViewBag.Message = "Number of requests exceeded. Insert new word to gain access";
                }
            }

            anagramList.Anagrams = anagrams;

            return View(anagramList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
   
}
