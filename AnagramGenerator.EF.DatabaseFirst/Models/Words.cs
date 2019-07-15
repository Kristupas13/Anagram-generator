using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class Words
    {
        public Words()
        {
            CachedWords = new HashSet<CachedWords>();
        }

        public int Id { get; set; }
        public string Word { get; set; }
        public string SortedWord { get; set; }

        public virtual ICollection<CachedWords> CachedWords { get; set; }
    }
}
