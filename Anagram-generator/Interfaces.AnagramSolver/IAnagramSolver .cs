using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface IAnagramSolver
    {
         IList<string> GetAnagrams(string myWords);
        IList<string> GetAnagramsSeperated(string myWords);
    }
}
