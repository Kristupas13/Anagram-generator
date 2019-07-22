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
        IList<UserLogEntity> GetUserLogListByIp(string ip);
        UserLogEntity Get(int userLogId);
        int Add(UserLogEntity userLogEntity);
        UserLogEntity Update(UserLogEntity userLogEntity);


    }
}
