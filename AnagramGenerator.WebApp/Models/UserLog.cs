using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class UserLog
    {
        public UserLogModel UserData { get; set; }
        public string IP { get; set; }
    }
}
