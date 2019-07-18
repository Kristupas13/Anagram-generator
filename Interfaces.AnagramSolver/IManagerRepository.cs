using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IManagerRepository
    {
        void TruncateTable(string tableName);
        bool WordExists(string word);


    }
}
