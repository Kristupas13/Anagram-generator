using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Services
{
    public class UserService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly ICacheRepository _cacheRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IManagerRepository managerRepository, IUserLogRepository userLogRepository, ICacheRepository cacheRepository, IUserRepository userRepository)
        {
            _managerRepository = managerRepository;
            _userLogRepository = userLogRepository;
            _cacheRepository = cacheRepository;
            _userRepository = userRepository;
        }

        public bool CheckIPLimit(string ip)
        {
            if (!_userRepository.UserExists(ip))
            {
                _userRepository.AddUser(ip);
            }

            return _userRepository.CheckIpLimit(ip);
        }
        public IList<UserLogModel> GetUserLog(string ip)
        {
            return _userLogRepository.GetUserLog(ip);
        }

        public void InsertToUserLog(int requestWordId, string IpAddress)
        {
            _userRepository.Decrement(IpAddress);
            _userLogRepository.InsertToUserLog(requestWordId, IpAddress);
        }

    }
}
