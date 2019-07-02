using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Interfaces.AnagramSolver;

namespace Implementation.AnagramSolver
{
    public class TextFileRepository : IFileRepository
    {
        string path;
        Dictionary<string, HashSet<string>> words = new Dictionary<string, HashSet<string>>();
        public TextFileRepository(string path)
        {
            this.path = path;
        }
        public Dictionary<string, HashSet<string>> Load()
        {
            string line;
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                char[] delimiterChars = {'\t'};
                string[] seperatedWords = line.Split(delimiterChars); // 0 - vardininkas, 1 - kalbos dalis, 2 - linksnis, 3 - skaicius
                if (words.ContainsKey(seperatedWords[1]))             
                {
          

                    words[seperatedWords[1]].Add(seperatedWords[0]);
                    words[seperatedWords[1]].Add(seperatedWords[2]);
                }
                else
                {
                    words[seperatedWords[1]] = new HashSet<string> { seperatedWords[0], seperatedWords[2] };
                }
            }
            return words; 
        }
    }
}
