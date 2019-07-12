using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts.Models
{
    public class WordModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string SortedWord { get; set; }
    }
}
