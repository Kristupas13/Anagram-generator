using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Interfaces.AnagramSolver;

namespace Implementation.AnagramSolver
{
    public class WordRepository : IWordRepository
    {
        private readonly Dictionary<string, HashSet<string>> wordsByPart;
        private readonly IFileRepository _fileRepository;

        public WordRepository(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            wordsByPart = _fileRepository.Load();
        }


        public Dictionary<string, HashSet<string>> GetWords()
        {
            return wordsByPart;
        }

    }
}
