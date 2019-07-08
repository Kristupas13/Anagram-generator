using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnagramGenerator.WebApp.Models;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IWordRepository _wordRepository;
        private readonly IList<string> DictionaryWords;
        private readonly HttpContextAccessor httpContextAccessor;

        public DictionaryController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            httpContextAccessor = new HttpContextAccessor();

            DictionaryWords = _wordRepository.GetAllWords();
        }

        public IActionResult Index(int page = 1, string searchedWord="")
        {
            var model = new PageSearchWord();

            ViewBag.Words = DictionaryWords.Skip((page-1)*100).Take(100);

            model.Page = page;
            model.SearchedWord = searchedWord;
        //    
            return View(model);
        }
        public IActionResult SearchPage(string searchWord)
        {
            int searchedWordPage = DictionaryWords.IndexOf(searchWord) / 100 + 1;

            Set(searchWord);

            return RedirectToAction("Index", new { page = searchedWordPage, searchedWord = searchWord});
        }



        public void Set(string value)
        {
            CookieOptions option = new CookieOptions();
            Response.Cookies.Append("key", value);
        }
        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }
        public string Get(string key)
        {
            return Request.Cookies[key];
        }
    }
}