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
        private readonly WordService _wordService;
        private readonly UserService _responseService;
        private readonly RequestService _requestService;
        public Extras(WordService wordServices, UserService responseService, RequestService requestService)
        {
            _wordService = wordServices;
            _responseService = responseService;
            _requestService = requestService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUserLogs(string address)
        {
            User log = new User() { UserLogs = _responseService.GetUserLog(address) };

            return View(log);
        }
        public IActionResult DeleteTable(string tableName ="")
        {
            if(!string.IsNullOrWhiteSpace(tableName))
                _requestService.TruncateTable(tableName);


            return View();
        }
    }
}