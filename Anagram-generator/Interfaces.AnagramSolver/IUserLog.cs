using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface IUserLog
    {
        void InsertToUserLog(string searchedWord, string IpAddress);
        IUserLog GetUserLog();
    }
}
