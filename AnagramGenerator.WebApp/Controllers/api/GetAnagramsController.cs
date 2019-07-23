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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GetAnagramsController : ControllerBase
    {
        private readonly IRequestService _requestService;
        public GetAnagramsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("{word}")]
        public IList<string> Get(string word)
        {
            IList<string> anagrams = _requestService.DetectAnagrams(word);
            return anagrams;
        }

    }
}