using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class CachedWords
    {
        public int Id { get; set; }
        public string SearchedWord { get; set; }
        public int AnagramId { get; set; }
    }
}
