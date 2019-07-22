using AnagramGenerator.Contracts.Models;
using AnagramGenerator.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Interfaces
{
    public interface IRequestRepository
    {
        IList<RequestEntity> GetAll();
        RequestEntity Get(int requestId);
        int Add(RequestEntity requestEntity);
        RequestEntity Update(RequestEntity requestEntity);

        RequestEntity GetByWord(string requestWord);
    }
}
