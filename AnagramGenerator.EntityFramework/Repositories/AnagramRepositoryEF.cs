using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.DatabaseFirst.Models;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class AnagramRepositoryEF : IAnagramRepository
    {
        AnagramDatabaseContext db;
        public AnagramRepositoryEF()
        {
            db = new AnagramDatabaseContext();
        }
        public WordModel GetWordModel(string phrase)
        {
            throw new NotImplementedException();
        }
    }
}
