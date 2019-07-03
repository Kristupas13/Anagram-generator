using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Interfaces.AnagramSolver;

namespace Implementation.AnagramSolver
{
    public class TextFileRepository : IFileRepository
    {
        private readonly string path;
        Dictionary<string, HashSet<string>> words = new Dictionary<string, HashSet<string>>();
        public TextFileRepository(string path)
        {
            this.path = path;
        }
        public Dictionary<string, HashSet<string>> Load()
        {
         //   string line;
       //     StreamReader file = new StreamReader(path);

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        char[] delimiterChars = { '\t' };
                        string[] seperatedWords = line.Split(delimiterChars); // 0 - vardininkas, 1 - kalbos dalis, 2 - linksnis, 3 - skaicius
                        var vardininkas = seperatedWords[0];
                        var kalbosDalis = seperatedWords[1];
                        var linksnis = seperatedWords[2];
                        if (words.ContainsKey(kalbosDalis))
                        {
                            words[kalbosDalis].Add(vardininkas);
                            words[kalbosDalis].Add(linksnis);
                        }
                        else
                        {
                            words[kalbosDalis] = new HashSet<string> { vardininkas, linksnis };
                        }
                    }

                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Your file could not be found!");
                Console.WriteLine(e.Message);
            }

            return words; 
        }
    }
}
