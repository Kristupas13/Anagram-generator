using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface IFileRepository
    {
        Dictionary<string, HashSet<string>> Load();
    }
}
