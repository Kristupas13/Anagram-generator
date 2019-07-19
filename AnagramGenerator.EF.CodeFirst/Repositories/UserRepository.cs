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
        private readonly CFDB_AnagramSolverContext _db;

        public UserRepository(CFDB_AnagramSolverContext db)
        {
            _db = db;
        }


        public IList<UserEntity> GetAll()
        {
            return _db.Users.ToList();
        }

        public UserEntity Get(int userId)
        {
            return _db.Users.Find(userId);
        }
        public UserEntity GetByIp(string userIp)
        {
            return _db.Users.SingleOrDefault(p => p.Ip == userIp);
        }
        public int Add(UserEntity userEntity)
        {
            _db.Users.Add(userEntity);
            _db.SaveChanges();
            return userEntity.Id;
        }

        public UserEntity Update(UserEntity userEntity)
        {
            UserEntity user = _db.Users.Single(p => p.Id == userEntity.Id);
            user.Ip = userEntity.Ip;
            user.Counter = userEntity.Counter;
            _db.SaveChanges();
            return user;
        }



        public UserEntity GetByIp(string ip)
        {
            return _db.Users.Where(q => q.Ip.Equals(ip)).FirstOrDefault();
        }

    }
}
