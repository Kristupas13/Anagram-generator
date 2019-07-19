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
        private readonly CFDB_AnagramSolverContext _db;
        public UserLogRepository(CFDB_AnagramSolverContext db)
        {
            _db = db;
        }

        public int Add(UserLogEntity userLogEntity)
        {
            _db.UserLogs.Add(userLogEntity);
            _db.SaveChanges();
            return userLogEntity.Id;
        }

        public UserLogEntity Get(int userLogId)
        {
            return _db.UserLogs.Find(userLogId);
        }

        public IList<UserLogEntity> GetAll()
        {
            return _db.UserLogs.ToList();
        }

        public IList<UserLogModel> GetUserLog(string ip)
        {

            throw new NotImplementedException();
        }

        public bool Contains(UserLogEntity userLogEntity)
        {
            return _db.UserLogs.Contains(userLogEntity);
        }

        public UserLogEntity Update(UserLogEntity userLogEntity)
        {
            throw new NotImplementedException();
        }

    }
}
