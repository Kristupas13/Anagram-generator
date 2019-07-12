﻿using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IUserLogRepository
    {
        void InsertToUserLog(string searchedWord, string IpAddress);
        IList<UserLogModel> GetUserLog(string ip);
    }
}
