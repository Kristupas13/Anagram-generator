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
        int Add(UserEntity userEntity);
        UserEntity Update(UserEntity userEntity);
        bool Contains(UserEntity requestEntity);


        void AddUser(string ip);
        bool CheckIpLimit(string ip);
        bool UserExists(string ip);
        void Increment(string ip);
        void Decrement(string ip);

    }
}
