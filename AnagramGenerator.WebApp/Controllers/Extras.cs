using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.WebApp.Models;
using AnagramGenerator.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers
{
    public class Extras : Controller
    {
        private readonly WordServices _wordService;
        public Extras(WordServices wordServices)
        {
            _wordService = wordServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUserLogs(string address)
        {
            User log = new User() { UserLogs = _wordService.GetUserLog(address) };

            return View(log);
        }
        public IActionResult DeleteTable(string tableName ="")
        {
            if(!string.IsNullOrWhiteSpace(tableName))
            _wordService.TruncateTable(tableName);


            return View();
        }
    }
}