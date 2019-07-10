using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class AnagramModel
    {
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramSolver _anagramSolver;

        public AnagramModel(IWordRepository wordRepository, IAnagramSolver anagramSolver)
        {
            _wordRepository = wordRepository;
            _anagramSolver = anagramSolver;

        }
        public IList<string> GetAllWords()
        {
            return _wordRepository.GetAllWords();
        }
    }
}
