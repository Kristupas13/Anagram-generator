using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Models
{
    public partial class CachedEntity
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int? AnagramId { get; set; }


        public virtual WordEntity Anagram { get; set; }
        public virtual RequestEntity Request { get; set; }
    }
}
