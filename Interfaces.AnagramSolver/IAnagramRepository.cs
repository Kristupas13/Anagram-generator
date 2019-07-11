using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IAnagramRepository
    {
        WordModel GetWordModel(string phrase);
    }
}
