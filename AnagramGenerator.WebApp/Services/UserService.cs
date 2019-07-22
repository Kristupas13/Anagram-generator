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


        public IList<UserLogModel> GetUserLogListByIp(string ip)
        {
            IList<UserLogModel> userLogs = new List<UserLogModel>();

            IList<UserLogEntity> logEntities = _userLogRepository.GetUserLogListByIp(ip);

            foreach(var log in logEntities)
            {
                UserLogModel logModel = new UserLogModel()
                {
                    Id = log.Id,
                    Date = log.Date,
                    RequestedWord = log.Request.Word,
                    UserIp = log.User.Ip
                };

                userLogs.Add(logModel);
            }

            return userLogs;
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


        }
        public IList<UserInfoModel> GetUserInformation(string ip)
        {
            UserEntity user = _userRepository.GetByIp(ip);
            if (user == null)
                return new List<UserInfoModel>();

            

            IList<UserInfoModel> listOfUserInformation = new List<UserInfoModel>();
            foreach(var userLog in user.UserLogEntity)
            {

                UserInfoModel infoModel = new UserInfoModel()
                {
                    UserIp = user.Ip,
                    Date = (DateTime)userLog.Date,
                    RequestedWord = userLog.Request.Word,
                    Anagrams = userLog.Request.CachedEntity.Select(p => p.Anagram.Word).ToList()
                };

                listOfUserInformation.Add(infoModel);
            }

            return listOfUserInformation;
        }

        public IList<string> GetAllAddresses()
        {
            var IPAddresses = _userRepository.GetAllListOfIps();

            return IPAddresses;
        }

        public void IncrementCounter(string ip)
        {
            UserEntity user = _userRepository.GetByIp(ip);
            user.Counter++;
            _userRepository.Update(user);
        }

        public void DecrementCounter(string ip)
        {
            UserEntity user = _userRepository.GetByIp(ip);
            user.Counter--;
            _userRepository.Update(user);
        }
    }
}
