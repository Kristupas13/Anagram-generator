using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts.Models
{
    public class CacheModel
    {
        public int Id { get; set; }
        public string SearchedWord { get; set; }
        public int AnagramId { get; set; }
    }
}
