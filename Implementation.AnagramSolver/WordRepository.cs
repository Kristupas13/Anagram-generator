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


        public Dictionary<string, HashSet<string>> GetDictionary()
        {
            return wordsByPart;
        }

        public List<string> GetAllWords()
        {
            List<string> allWords = new List<string>();
            foreach (var contents in wordsByPart.Keys)
            {
                foreach (var listMember in wordsByPart[contents])
                {
                    allWords.Add(listMember);
                }
            }

            return allWords;
        }
    }
}
