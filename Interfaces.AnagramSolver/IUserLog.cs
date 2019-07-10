using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface IUserLog
    {
         string IpAddress { get; set; }
         DateTime Date { get; set; }
         HashSet<string> SearchedWord { get; set; }
         HashSet<string> Anagrams { get; set; }

        void InsertToUserLog(string searchedWord, string IpAddress);
        IUserLog GetUserLog(string ip);
    }
}
