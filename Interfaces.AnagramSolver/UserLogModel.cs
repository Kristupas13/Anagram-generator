using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public class UserLogModel
    {
        public string UserIP { get; set; }
        public DateTime Date { get; set; }
        public string Word { get; set; }
        public IList<WordModel> Anagrams { get; set; }
    }
}
