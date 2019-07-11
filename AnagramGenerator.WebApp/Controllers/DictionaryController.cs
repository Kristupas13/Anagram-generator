using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnagramGenerator.WebApp.Models;
using AnagramGenerator.Contracts;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Services;

namespace AnagramGenerator.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IWordRepository _wordRepository;
        public DictionaryController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IActionResult Index(int page = 1, string searchedWord="")
        {
            var model = new PageSearchWord();

            if(!String.IsNullOrWhiteSpace(searchedWord))

            ViewBag.Words = _wordRepository.FindByWordPart(searchedWord);

            else

            ViewBag.Words = _wordRepository.LoadWords(page);

            model.Page = page;
            model.SearchedWord = searchedWord;
           
            return View(model);
        }
        public IActionResult SearchPage(string searchWord)
        {
            Set(searchWord);

            return RedirectToAction("Index", new { page = 1, searchedWord = searchWord});
        }


        public void Set(string value)
        {
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