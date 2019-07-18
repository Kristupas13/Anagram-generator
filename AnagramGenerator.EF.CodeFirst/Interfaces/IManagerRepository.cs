using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Interfaces
{
    public interface IManagerRepository
    {
        void TruncateTable(string tableName);


    }
}
