using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface IWordRepository
    {
        Dictionary<string, HashSet<string>> getWordRepository();   
    }
}
