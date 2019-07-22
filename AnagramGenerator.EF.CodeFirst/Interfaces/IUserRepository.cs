using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Interfaces
{
    public interface IUserRepository
    {
        IList<UserEntity> GetAll();
        UserEntity Get(int userId);
        UserEntity GetByIp(string ip);
        IList<string> GetAllListOfIps();
        int Add(UserEntity userEntity);
        UserEntity Update(UserEntity userEntity);


    }
}
