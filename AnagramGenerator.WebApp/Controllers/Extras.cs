using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.WebApp.Models;
using AnagramGenerator.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers
{
    public class Extras : Controller
    {
        private readonly IModificationService _wordService;
        private readonly IUserService _responseService;
        private readonly IRequestService _requestService;
        public Extras(IModificationService wordServices, IUserService responseService, IRequestService requestService)
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