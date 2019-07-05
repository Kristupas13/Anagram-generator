using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Models;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;


namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramSolver _anagramSolver;
          
        public HomeController()
        {
            _fileRepository = new TextFileRepository(@"C:\Users\kristupas\Desktop\UpdatedDataManipulation\Data_Manipulation-master\Data_Manipulation-master\MainApp\zodynas.txt");      // testing
            _wordRepository = new WordRepository(_fileRepository);
            _anagramSolver = new AnagramSolver(_wordRepository);
        }
        public IActionResult Index(string phrase = "")
        {
            ViewData["Message"] = phrase;
           

            ViewBag.Anagrams = _anagramSolver.GetAnagrams(phrase);

            return View();
        }
        public IList<string> GetAnagrams(string phrase)
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
