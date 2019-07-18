using AnagramGenerator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

using AnagramGenerator.EF.CodeFirst.Models;
using System.Linq;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class UserRepository : IUserRepository
    {
        CFDB_AnagramSolverContext db;
        public UserRepository()
        {
            db = new CFDB_AnagramSolverContext();
        }
        public bool UserExists(string ip)
        {
            return db.Users.Where(p => p.Ip == ip).Any();
        }
        public void AddUser(string ip)
        {
            UserEntity user = new UserEntity()
            {
                Ip = ip
            };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public bool CheckIpLimit(string ip)
        {
            return db.Users.Where(p => p.Ip == ip).Select(p => p.Counter >= 0).Any();
        }

        public void Increment(string ip)
        {
            UserEntity user = db.Users.Single(p => p.Ip == ip);
            user.Counter++;
            db.SaveChanges();
        }

        public void Decrement(string ip)
        {
            UserEntity user = db.Users.Single(p => p.Ip == ip);
            user.Counter--;
            db.SaveChanges();
        }
    }
}
