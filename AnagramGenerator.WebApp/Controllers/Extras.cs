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
        private readonly IModificationService _modificationService;
        private readonly IUserService _userService;
        private readonly IRequestService _requestService;
        public Extras(IModificationService modificationService, IUserService userService, IRequestService requestService)
        {
            _modificationService = modificationService;
            _userService = userService;
            _requestService = requestService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUserLogs(string address)
        {
            UserSearchInformation log = new UserSearchInformation() { UserLogs = _userService.GetUserInformation(address) };

            return View(log);
        }
        public IActionResult DeleteTable(string tableName ="")
        {
            if(!string.IsNullOrWhiteSpace(tableName))
                _modificationService.TruncateTable(tableName);


            return View();
        }
    }
}