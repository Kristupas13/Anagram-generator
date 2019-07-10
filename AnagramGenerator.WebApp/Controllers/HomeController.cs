using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Models;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;


namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAnagramSolver _anagramSolver;
        private readonly ICacheRepository _cacheRepository;
        private readonly IUserLog _userLog;
        private readonly string connectionString;
          
        public HomeController(IAnagramSolver anagramSolver, ICacheRepository cacheRepository, IUserLog userLog ,IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("connectionString");
     
           
            _anagramSolver = anagramSolver;         
            _cacheRepository = cacheRepository;
            _userLog = userLog;
        }
        public IActionResult Index(string phrase = "")
        {
            AnagramList AnagramModel;

            if (!String.IsNullOrWhiteSpace(phrase))
            {
                IList<string> anagrams = _cacheRepository.CheckCached(phrase);
                if(anagrams.Any())
                {
                    AnagramModel = new AnagramList() { Anagrams = anagrams };
                }
                else
                {
                    anagrams = _anagramSolver.GetAnagramsSeperated(phrase);
                    if(anagrams.Any())
                    {
                        _cacheRepository.InsertWordToCache(phrase, anagrams);
                        AnagramModel = new AnagramList() { Anagrams = anagrams};

                    }
                    else
                    AnagramModel = new AnagramList() { Anagrams = new List<string>() };

                }

                _userLog.InsertToUserLog(phrase, HttpContext.Connection.LocalIpAddress.ToString());
               IUserLog useris =_userLog.GetUserLog(HttpContext.Connection.LocalIpAddress.ToString());
            }

            else
            {
                AnagramModel = new AnagramList() { Anagrams = new List<string>() }; // bad
            }
            return View(AnagramModel);
        }
        public IActionResult GetInfo(string ip)
        {
            return new EmptyResult();
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
