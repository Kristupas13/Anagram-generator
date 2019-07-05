using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnagramGenerator.WebApp.Models;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramSolver _anagramSolver;
        private readonly IList<string> DictionaryWords;

        public DictionaryController()
        {
            _fileRepository = new TextFileRepository(@"C:\Users\kristupas\Desktop\UpdatedDataManipulation\Data_Manipulation-master\Data_Manipulation-master\MainApp\zodynas.txt");      // testing
            _wordRepository = new WordRepository(_fileRepository);
            _anagramSolver = new AnagramSolver(_wordRepository);
            DictionaryWords = _wordRepository.GetAllWords();


        }

        public IActionResult Index(int page = 1)
        {
            var model = new PageNumber();

            ViewBag.Words = DictionaryWords.Skip((page-1)*100).Take(100);

            model.Page = (page - 1) * 100 + 1;
            return View(model);
        }
        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\kristupas\Desktop\UpdatedDataManipulation\Data_Manipulation-master\Data_Manipulation-master\MainApp\zodynas.txt");
            string fileName = "Dictionary.ext";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}