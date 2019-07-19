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
        RequestEntity GetByWord(string requestWord);
        int Add(RequestEntity requestEntity);
        RequestEntity Update(RequestEntity requestEntity);

    }
}
