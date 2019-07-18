using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class PageSearchWord
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public string SearchedWord { get; set; }

        public IList<string> PageWords { get; set; }
    }
}
