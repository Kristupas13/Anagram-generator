using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Models;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using Microsoft.AspNetCore.Http;

namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly IHttpContextAccessor _httpContextAccessor;
          
        public HomeController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
            _httpContextAccessor = new HttpContextAccessor();
        }
        public IActionResult Index(string phrase = "")
        {


            ViewData["Message"] = phrase;

            var Anagrams = new AnagramList { Anagrams = _anagramSolver.GetAnagrams(phrase) };




            return View(Anagrams);
        }
        public IList<string> GetAnagrams(string phrase = "")
        {
            return _anagramSolver.GetAnagrams(phrase);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
