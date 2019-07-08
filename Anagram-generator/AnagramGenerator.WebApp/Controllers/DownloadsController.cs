using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers
{
    public class DownloadsController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public FileResult DownloadDictionary()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\kristupas\Desktop\UpdatedDataManipulation\Data_Manipulation-master\MainApp\zodynas.txt");
            string fileName = "Dictionary.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}