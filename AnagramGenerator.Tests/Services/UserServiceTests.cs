using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst.Models;
using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.WebApp.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserLogRepository _userLogRepository;
        private IUserRepository _userRepository;
        private IRequestRepository _requestRepository;
        private IUserService _userService;
        private string ipAddress;

        [SetUp]
        public void Setup()
        {
            _userLogRepository = Substitute.For<IUserLogRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _requestRepository = Substitute.For<IRequestRepository>();

            _userService = new UserService(_userLogRepository, _userRepository, _requestRepository);
        }

        [Test]
        public void CheckIpLimit_ShouldReturnTrueIfCounterIsGreaterThanZero()
        {
            ipAddress = "::1";
            _userRepository.GetByIp(ipAddress).Returns(new UserEntity
            {
                Counter = 4
            });

            //////////////////////////////////
            //////////////////////////////////

            var result = _userService.CheckIPLimit(ipAddress);

            result.ShouldNotBeNull();
            result.ShouldBeTrue();
            
            

            _userRepository.Received().GetByIp(ipAddress);
        }
        [Test]
        public void CheckIpLimit_ShouldReturnFalseIfCounterIsLowerThanZero()
        {
            ipAddress = "::1";
            _userRepository.GetByIp(ipAddress).Returns(new UserEntity
            {
                Counter = -1,
            });

            //////////////////////////////////
            //////////////////////////////////

            var result = _userService.CheckIPLimit(ipAddress);
           
            result.ShouldNotBeNull();
            result.ShouldBeFalse();

            _userRepository.Received().GetByIp(ipAddress);
        }
        [Test]
        public void CheckIpLimit_ShouldReturnFalseIfCounterIsEqualToZero()
        {
            ipAddress = "::1";
            _userRepository.GetByIp(ipAddress).Returns(new UserEntity
            {
                Counter = 0
            });
            //////////////////////////////////
            //////////////////////////////////

            var result = _userService.CheckIPLimit(ipAddress);
            
            result.ShouldNotBeNull();
            result.ShouldBeFalse();

            _userRepository.Received().GetByIp(ipAddress);
        }


        [Test]
        public void GetUserLogListByIp_ShouldReturnUserLogListByIp()
        {
            ipAddress = "::1";

            UserLogModel expected = new UserLogModel()
            {
                RequestedWord = "labas",
                UserIp = "::1",
            };

            var sample = new UserLogEntity()
            {
                Id = 1,
                User = new UserEntity() { Id = 1, Ip = "::1" },
                Request = new RequestEntity() { Id = 1, Word = "labas" },
                Date = DateTime.Now
            };

                       
            _userLogRepository.GetUserLogListByIp(ipAddress).Returns(new List<UserLogEntity>
            {
                   sample,
               new UserLogEntity()
               {       
                   Id = 2,
                   User = new UserEntity(){Id = 1, Ip = "::1"},
                   Request = new RequestEntity(){Id = 2, Word = "Labas"},
                   Date = DateTime.Now
               },
                  new UserLogEntity()
               {
                   Id = 3,
                   User = new UserEntity(){Id = 2, Ip = "::2"},
                   Request = new RequestEntity(){Id = 2, Word = "Labas"},
                   Date = DateTime.Now
               },
            });

            //////////////////////////////////
            //////////////////////////////////


            var result = _userService.GetUserLogListByIp(ipAddress);


            result.ShouldNotBeNull();

            sample.User.Ip.ShouldBeSameAs(expected.UserIp);
/*            sample.Request.Id.ShouldBeSameAs()*/


            _userLogRepository.Received().GetUserLogListByIp(ipAddress);
        }

        [Test]
        public void GetAllAddresess_ShouldReturnEveryIpAddress()
        {
            _userRepository.GetAllListOfIps().Returns(new List<string>()
            {
                "::1",
                "::2",
                "127.0.0.1",
                "123.354.621.3"
            });

            //////////////////////////////////
            //////////////////////////////////

            var result = _userService.GetAllAddresses();

            result.ShouldNotBeEmpty();

            result[0].ShouldBeSameAs("::1");
            result[1].ShouldBeSameAs("::2");
            result[2].ShouldBeSameAs("127.0.0.1");
            result[3].ShouldBeSameAs("123.354.621.3");

            _userRepository.Received().GetAllListOfIps();
        }

        [Test]
        public void GetAllAddresess_ShouldReturnNullIfEmpty()
        {
            _userRepository.GetAllListOfIps().Returns(new List<string>()
            {
               
            });

            //////////////////////////////////
            //////////////////////////////////

            var result = _userService.GetAllAddresses();

            result.ShouldBeEmpty();

            _userRepository.Received().GetAllListOfIps();
        }
        [Test]
        public void IncrementCounter_ShouldIncrementCounter()
        {
            ipAddress = "::1";
            int counter = 0;

            UserEntity userEntity = new UserEntity()
            {
                Id = 1,
                Ip = "::1",
                Counter = counter
            };       
            
            _userRepository.GetByIp(ipAddress).Returns(userEntity);

            //////////////////////////////////
            //////////////////////////////////
            
            _userService.IncrementCounter(ipAddress);


            userEntity.Ip.ShouldBe(ipAddress);
            userEntity.Id.ShouldBe(1);

            userEntity.Counter.ShouldBe(counter+1);

            _userRepository.Received().GetByIp(ipAddress);
        }
        [Test]
        public void DecrementCounter_ShouldDecrementCounter()
        {
            ipAddress = "::1";
            int counter = -1;

            UserEntity userEntity = new UserEntity()
            {
                Id = 1,
                Ip = "::1",
                Counter = counter
            };

            _userRepository.GetByIp(ipAddress).Returns(userEntity);

            //////////////////////////////////
            //////////////////////////////////

            _userService.DecrementCounter(ipAddress);


            userEntity.Ip.ShouldBe(ipAddress);
            userEntity.Id.ShouldBe(1);

            userEntity.Counter.ShouldBe(counter - 1);

            _userRepository.Received().GetByIp(ipAddress);
        }
    }
}
