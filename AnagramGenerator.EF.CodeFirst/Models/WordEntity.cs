using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Models
{
    public partial class WordEntity
    {
        public WordEntity()
        {
            CachedEntity = new HashSet<CachedEntity>();
        }

        public int Id { get; set; }
        public string Word { get; set; }
        public string SortedWord { get; set; }

        public virtual ICollection<CachedEntity> CachedEntity { get; set; }
    }
}
