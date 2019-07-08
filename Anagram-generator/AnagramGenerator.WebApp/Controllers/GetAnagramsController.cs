using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAnagramsController : ControllerBase
    {
        private readonly IAnagramSolver _anagramSolver;
        public GetAnagramsController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
        }

        [HttpGet("{name}")]
        public IList<string> Get(string name)
        {
            IList<string> anagrams = _anagramSolver.GetAnagrams(name);
            return anagrams;

        }

    }
}