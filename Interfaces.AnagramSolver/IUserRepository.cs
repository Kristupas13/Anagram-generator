using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IUserRepository
    {
        void AddUser(string ip);
        bool CheckIpLimit(string ip);
        bool UserExists(string ip);
        void Increment(string ip);
        void Decrement(string ip);

    }
}
