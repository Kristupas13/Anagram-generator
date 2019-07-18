using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Services
{
    public interface IRequestService
    {

        IList<WordModel> ConvertCacheModelToWordModel(IList<CacheModel> cacheModels);


        IList<WordModel> DetectAnagrams(string requestWord);

        void TruncateTable(string tableName);

        IList<CacheModel> GetCachedByRequestId(int requestId);










        RequestModel GetRequestModel(string word);
        RequestModel GetExistingRequestModel(string word);
        RequestModel AddRequestModel(string word);
    }
}
