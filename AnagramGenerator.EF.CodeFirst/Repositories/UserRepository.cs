using AnagramGenerator.EF.CodeFirst.Interfaces;
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


        public IList<UserEntity> GetAll()
        {
            return db.Users.ToList();
        }

        public UserEntity Get(int userId)
        {
            return db.Users.Find(userId);
        }

        public int Add(UserEntity userEntity)
        {
            db.Users.Add(userEntity);
            db.SaveChanges();
            return userEntity.Id;
        }

        public UserEntity Update(UserEntity userEntity)
        {
            UserEntity user = db.Users.Single(p => p.Id == userEntity.Id);
            user.Ip = userEntity.Ip;
            user.Counter = userEntity.Counter;
            db.SaveChanges();
            return user;

        }


        public bool Contains(UserEntity userEntity)
        {
            return db.Users.Contains(userEntity);
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
            return db.Users.Where(p => p.Ip == ip).Any(p => p.Counter > 0);
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
