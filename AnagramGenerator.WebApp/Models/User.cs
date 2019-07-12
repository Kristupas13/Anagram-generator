using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class User
    {
        public IList<UserLogModel> UserLogs { get; set; }
    }
}
