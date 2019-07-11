using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IUserLogRepository
    {
        void InsertToUserLog(WordModel searchedWord, string IpAddress);
        IList<UserLogModel> GetUserLog(string ip);
    }
}
