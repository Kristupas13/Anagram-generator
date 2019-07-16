using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Models
{
    public partial class UserLogEntity
    {


        public int Id { get; set; }
        public string UserIp { get; set; }
        public string SearchedWord { get; set; }
        public DateTime? Date { get; set; }

    }
}
