using AnagramGenerator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class AnagramList
    {
        public IList<WordModel> Anagrams { get; set; }
    }
}
