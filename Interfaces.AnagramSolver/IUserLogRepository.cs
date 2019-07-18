using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IUserLogRepository
    {
        void InsertToUserLog(int requestWordId, string IpAddress);
        IList<UserLogModel> GetUserLog(string ip);
        bool UserIPLimit(string ip);

    }
}
