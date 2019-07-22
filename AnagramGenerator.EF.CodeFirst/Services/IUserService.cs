using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Services
{
    public interface IUserService
    {
        bool CheckIPLimit(string ip);

        IList<UserLogModel> GetUserLogListByIp(string ip);

        void InsertToUserLog(string requestWord, string IpAddress);

        IList<UserInfoModel> GetUserInformation(string ip);

        IList<string> GetAllAddresses();

        void IncrementCounter(string ip);

        void DecrementCounter(string ip);

    }
}
