using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MainApp
{
    public static class Config
    {
        public static int MinWordLength
        {
            get { return int.Parse(ConfigurationManager.AppSettings["minWordLength"]); }
        }
        public static int MaxWords
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["maxAnagrams"]);
            }
        }
    }
}
