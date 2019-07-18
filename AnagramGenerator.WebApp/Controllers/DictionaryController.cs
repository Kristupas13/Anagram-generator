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
        private readonly WordService _wordService;
        public DictionaryController(WordService wordService)
        {
            _wordService = wordService;
        }

        public IActionResult Index(int page = 1, string searchedWord="")
        {
            var model = new PageSearchWord();
            

            if(!String.IsNullOrWhiteSpace(searchedWord))

            {
                SetCookie(searchedWord);
                model.PageWords = _wordService.Find(searchedWord);
            }

            else
            model.PageWords = _wordService.LoadWords(page);

            model.Page = page;
            model.SearchedWord = searchedWord;
           
            return View(model);
        }
        public IActionResult SearchPage(string searchWord)
        {
            return RedirectToAction("Index", new { page = 1, searchedWord = searchWord});
        }

        public IActionResult Add(string word = "")
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                _wordService.AddWord(word, HttpContext.Connection.RemoteIpAddress.ToString());
                ViewBag.Message = "Word added";
            }
            return View();
        }

        public IActionResult Remove(string word = "")
        {
            if(!string.IsNullOrWhiteSpace(word))
            {
                _wordService.RemoveWord(word, HttpContext.Connection.RemoteIpAddress.ToString());
                ViewBag.Message = "Word removed";
            }
            return View();
        }

        public void SetCookie(string value)
        {
            Response.Cookies.Append("Last", value);
        }
        public void RemoveCookie(string key)
        {
            Response.Cookies.Delete(key);
        }
        public string GetCookie(string key)
        {
            return Request.Cookies[key];
        }
    }
}