using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Interfaces
{
    public interface IUserLogRepository
    {
        IList<UserLogEntity> GetAll();
        UserLogEntity Get(int userLogId);
        int Add(UserLogEntity userLogEntity);
        UserLogEntity Update(UserLogEntity userLogEntity);
        bool Contains(UserLogEntity requestEntity);


        void InsertToUserLog(int requestWordId, string IpAddress);
        IList<UserLogModel> GetUserLog(string ip);
        bool UserIPLimit(string ip);

    }
}
