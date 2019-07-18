using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnagramGenerator.WebApp.Models;
using AnagramGenerator.Contracts;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Services;
using AnagramGenerator.EF.CodeFirst.Services;

namespace AnagramGenerator.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IModificationService _wordService;
        private readonly IUserService _userService;
        public DictionaryController(IModificationService wordService, IUserService userService)
        {
            _wordService = wordService;
            _userService = userService;
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
                string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                bool addedCorrectly = _wordService.AddWord(word, ipAddress);

                if(addedCorrectly)
                {
                    _userService.IncrementCounter(ipAddress);
                    ViewBag.Message = "Word added";
                }
                else
                ViewBag.Message = "Error occured";

            }
            return View();
        }

        public IActionResult Remove(string word = "")
        {
            if(!string.IsNullOrWhiteSpace(word))
            {
                string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                bool removedCorrectly = _wordService.RemoveWord(word, ipAddress);
                if(removedCorrectly)
                {
                    _userService.DecrementCounter(ipAddress);
                    ViewBag.Message = "Word removed";
                }
                else
                ViewBag.Message = "Error occured";
            }
            return View();
        }
        public IActionResult Edit(string oldWord = "", string newWord = " ")
        {
            if (!(string.IsNullOrWhiteSpace(oldWord) && string.IsNullOrWhiteSpace(newWord)))
            {
                string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                bool editedCorrectly = _wordService.EditWord(oldWord, newWord, ipAddress);
                if (editedCorrectly)
                {
                    _userService.IncrementCounter(ipAddress);
                    ViewBag.Message = "Word edited";
                }

                ViewBag.Message = "Error occured";
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