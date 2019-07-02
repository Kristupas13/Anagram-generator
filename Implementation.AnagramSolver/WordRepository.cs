using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Interfaces.AnagramSolver;

namespace Implementation.AnagramSolver
{
    public class WordRepository : IWordRepository
    {
        private Dictionary<string, HashSet<string>> wordsByPart;
        public WordRepository(LoadRespository LoadRespository)
        {
            wordsByPart = LoadRespository.Load();
        }
        public Dictionary<string, HashSet<string>> getWordRepository()
        {
            return wordsByPart;
        }

    }
}
