using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface LoadRespository
    {
        Dictionary<string, HashSet<string>> Load();
    }
}
