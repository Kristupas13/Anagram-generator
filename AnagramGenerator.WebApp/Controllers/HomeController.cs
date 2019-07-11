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

namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAnagramSolver _anagramSolver;
        private readonly WordServices wordServices;
        private readonly string connectionString;
          
        public HomeController(IAnagramSolver anagramSolver, WordServices wordServices, IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("connectionString");

            this.wordServices = wordServices;  
            _anagramSolver = anagramSolver;         

        }
        public IActionResult Index(string phrase = "")
        {
            AnagramList anagramModel = new AnagramList();
            if (!String.IsNullOrWhiteSpace(phrase))
            {
               IList<WordModel> wm = wordServices.GetWordModel(phrase);

                IList<WordModel> anagrams = wordServices.CheckCached(wm);

                if(anagrams.Any())
                {
                    anagramModel.Anagrams = anagrams;
                    wordServices.InsertToUserLog(wm, HttpContext.Connection.LocalIpAddress.ToString());
                    return View(anagramModel);
                }
                else
                {
                    anagrams = _anagramSolver.GetAnagramsSeperated(phrase);
                    if(anagrams.Any())
                    {
                        wordServices.InsertWordToCache(wm, anagrams);
                        wordServices.InsertToUserLog(wm, HttpContext.Connection.LocalIpAddress.ToString());
                        anagramModel.Anagrams = anagrams;
                        return View(anagramModel);

                    }
                    else
                        anagramModel = new AnagramList() { Anagrams = new List<WordModel>() };

                }

            }

            else
            {
                anagramModel = new AnagramList() { Anagrams = new List<WordModel>() };
            }
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
