using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
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
        public IList<WordModel> Get(string name)
        {
            IList<WordModel> anagrams = _anagramSolver.GetAnagramsSeperated(name);
            return anagrams;

        }

    }
}