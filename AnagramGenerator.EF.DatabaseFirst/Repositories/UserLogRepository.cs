using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        AnagramDatabaseContext db;
        public UserLogRepository()
        {
            db = new AnagramDatabaseContext();
        }
        public IList<UserLogModel> GetUserLog(string ip)
        {
            var q = db.UserLog.Where(p => p.UserIp == ip).Select(p => new UserLogModel() { UserIp = ip, Id = p.Id, Date = p.Date }).ToList();
            return q;
        }

        public void InsertToUserLog(int requestWordId, string IpAddress)
        {

         /*   UserLog userLog = new UserLog()
            {
                Date = DateTime.Now,
                SearchedWord = searchedWord,
                UserIp = IpAddress
            }; 
            db.UserLog.Add(userLog);
            db.SaveChanges();*/
        }
        public bool UserIPLimit(string ip)
        {
            throw new NotImplementedException();
        }

    }
}
