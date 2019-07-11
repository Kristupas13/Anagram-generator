using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public class CachedWordModel
    {
        public int ID { get; set; }
        public string Word { get; set; }
        public List<int> AnagramID { get; set; }
    }
}
