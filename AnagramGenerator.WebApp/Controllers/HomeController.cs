using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Models;
using AnagramGenerator.Contracts;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using AnagramGenerator.WebApp.Services;
using AnagramGenerator.Contracts.Models;

namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly WordServices wordServices;
        private readonly string connectionString;
          
        public HomeController(WordServices wordServices, IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("connectionString");

            this.wordServices = wordServices;       

        }
        public IActionResult Index(string phrase = "")
        {
            AnagramList anagramModel;

            if (!String.IsNullOrWhiteSpace(phrase))
            {

                IList<CacheModel> cachedWords = wordServices.CheckCached(phrase);

                if(cachedWords.Any())
                {
                    wordServices.InsertToUserLog(phrase, HttpContext.Connection.LocalIpAddress.ToString());

                    anagramModel = new AnagramList() { Anagrams = wordServices.ConvertCachedToWords(cachedWords) };
                }
                else
                {
                    wordServices.InsertToUserLog(phrase, HttpContext.Connection.LocalIpAddress.ToString());

                    anagramModel = new AnagramList(){ Anagrams = wordServices.FindAnagrams(phrase)};             
                }
            }

            else
                anagramModel = new AnagramList() { Anagrams = new List<WordModel>()};


            return View(anagramModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }







        public void TruncateTable(string tableName)
        {
            string spName = @"dbo.[DeleteTable]";
            using(SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(spName, cn);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = @"TableName";
                param1.SqlDbType = SqlDbType.NVarChar;
                param1.Value = tableName;
                cmd.Parameters.Add(param1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

    }
   
}
