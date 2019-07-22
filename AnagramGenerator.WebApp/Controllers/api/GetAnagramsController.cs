using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAnagramsController : ControllerBase
    {
        private readonly IRequestService _requestService;
        public GetAnagramsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("{name}")]
        public IList<string> Get(string name)
        {
            IList<string> wordModels = _requestService.DetectAnagrams(name);

            IList<string> anagrams = new List<string>();

            foreach(var item in wordModels)
            {
                anagrams.Add(item);
            }

            return anagrams;

        }

    }
}