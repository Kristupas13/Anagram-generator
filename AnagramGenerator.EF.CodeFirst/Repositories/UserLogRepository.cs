using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        CFDB_AnagramSolverContext db;
        public UserLogRepository()
        {
            db = new CFDB_AnagramSolverContext();
        }

        public int Add(UserLogEntity userLogEntity)
        {
            db.UserLogs.Add(userLogEntity);
            db.SaveChanges();
            return userLogEntity.Id;
        }

        public UserLogEntity Get(int userLogId)
        {
            return db.UserLogs.Find(userLogId);
        }

        public IList<UserLogEntity> GetAll()
        {
            return db.UserLogs.ToList();
        }

        public IList<UserLogModel> GetUserLog(string ip)
        {

            throw new NotImplementedException();
        }

        public bool Contains(UserLogEntity userLogEntity)
        {
            return db.UserLogs.Contains(userLogEntity);
        }

        public UserLogEntity Update(UserLogEntity userLogEntity)
        {
            throw new NotImplementedException();
        }










        public void InsertToUserLog(int requestWordId, string IpAddress)
        {
            UserLogEntity userLogEntity = new UserLogEntity()
            {
                UserIp = IpAddress,
                RequestId = requestWordId,
                Date = DateTime.Now,
                UserId = db.Users.Where(p => p.Ip == IpAddress).Select(p => p.Id).FirstOrDefault(),
            };

            db.UserLogs.Add(userLogEntity);
            db.SaveChanges();
        }



        public bool UserIPLimit(string ip)
        {
            var q = db.UserLogs.Select(p => p.UserIp == ip).Skip(3).Any();
            return !q;
        }
    }
}
