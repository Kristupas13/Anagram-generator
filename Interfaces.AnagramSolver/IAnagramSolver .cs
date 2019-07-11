using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IAnagramSolver
    {
      /*   IList<string> GetAnagrams(string myWords);*/
        IList<WordModel> GetAnagramsSeperated(string myWords);
    }
}
