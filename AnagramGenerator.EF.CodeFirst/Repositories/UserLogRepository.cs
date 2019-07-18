using AnagramGenerator.Contracts;
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
        public IList<UserLogModel> GetUserLog(string ip)
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
