using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst.Models;

namespace AnagramGenerator.WebApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserLogRepository _userLogRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRequestRepository _requestRepository;


        public UserService(IUserLogRepository userLogRepository, IUserRepository userRepository, IRequestRepository requestRepository)
        {
            _userLogRepository = userLogRepository;
            _userRepository = userRepository;
            _requestRepository = requestRepository;
        }

        public bool CheckIPLimit(string ip)
        {

            UserEntity userEntity = _userRepository.GetByIp(ip);

            if (userEntity == null)
            {
                userEntity = new UserEntity()
                {
                    Ip = ip,
                    Counter = 4,
                };
                _userRepository.Add(userEntity);
            }

            bool grantAccess = _userRepository.GetByIp(ip).Counter > 0;

             return grantAccess;
        }


        public IList<UserLogModel> GetUserLog(string ip)
        {
            return _userLogRepository.GetAll().Where(p => p.User.Ip == ip).Select(p => new UserLogModel() { Date = p.Date, Id = p.Id, RequestId = p.RequestId, UserIp = p.UserIp }).ToList();
        }

        public void InsertToUserLog(string requestWord, string IpAddress)
        {


            UserLogEntity userLogEntity = new UserLogEntity()
            {
                Date = DateTime.Now,
                RequestId = _requestRepository.GetByWord(requestWord).Id,
                UserId = _userRepository.GetByIp(IpAddress).Id,
                UserIp = IpAddress
            };

            _userLogRepository.Add(userLogEntity);

            DecrementCounter(IpAddress);

        }
        public void IncrementCounter(string ip)
        {
            UserEntity user = _userRepository.GetAll().Where(p => p.Ip == ip).Single();
            user.Counter++;
            _userRepository.Update(user);
        }
        public void DecrementCounter(string ip)
        {
            UserEntity user = _userRepository.GetAll().Where(p => p.Ip == ip).Single();
            user.Counter--;
            _userRepository.Update(user);
        }
    }
}
