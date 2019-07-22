using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts.Models
{
    public class UserInfoModel
    {
        public string UserIp { get; set; }
        public DateTime Date { get; set; }
        public string RequestedWord { get; set; }
        public IList<string> Anagrams { get; set; }
    }
}
