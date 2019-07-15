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
        Solver_DBContext db;
        public UserLogRepository()
        {
            db = new Solver_DBContext();
        }
        public IList<UserLogModel> GetUserLog(string ip)
        {
            /* var q = db.UserLogs.Where(p => p.UserIp == ip).Select(p => new UserLogModel() { UserIp = ip, SearchedWord = p.SearchedWord, Id = p.Id, Date = p.Date }).ToList();


                        SELECT UserIp, Date, cw.SearchedWord, Word
                          FROM Words w
                          inner join CachedWords cw
                             on w.Id = cw.AnagramID
                             inner join UserLogs u
                               on u.UserIP = '::1'
                                  WHERE cw.SearchedWord = u.SearchedWord
                                   ORDER BY Date  */

            var query = from words in db.Words
                        join cachedWords in db.CachedWords on words.Id equals cachedWords.AnagramId
                        from logs in db.UserLogs
                        where logs.UserIp == ip && logs.SearchedWord == cachedWords.SearchedWord
                        select new { logs.UserIp, logs.Date, words.Word, logs.SearchedWord };

            var test = query.Select(p => new UserLogModel() { SearchedWord = p.SearchedWord, Date = p.Date, UserIp = ip }).ToList();

            foreach(var item in query)
            {
                
            }


            return test;
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
