using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts.Models
{
    public class CacheModel
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int AnagramId { get; set; }
    }
}
