using AnagramGenerator.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IRequestRepository
    {
        void Add(string word);
        int GetID(string word);
        bool Exists(string word);
        RequestModel ToModel(string word);
        RequestModel ToModel(int  id);
    }
}
