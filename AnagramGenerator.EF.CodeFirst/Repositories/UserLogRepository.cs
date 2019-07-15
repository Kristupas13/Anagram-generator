using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        Solver_DBContext db;
        public UserLogRepository()
        {
            db = new Solver_DBContext();
        }
        public IList<UserLogModel> GetUserLog(string ip)
        {
            var q = db.UserLogs.Where(p => p.UserIp == ip).Select(p => new UserLogModel() { UserIp = ip, SearchedWord = p.SearchedWord, Id = p.Id, Date = p.Date }).ToList();
            return q;
        }

        public void InsertToUserLog(string searchedWord, string IpAddress)
        {

            UserLogEntity userLog = new UserLogEntity()
            {
                Date = DateTime.Now,
                SearchedWord = searchedWord,
                UserIp = IpAddress
            };
            db.UserLogs.Add(userLog);
            db.SaveChanges();
        }
    }
}
