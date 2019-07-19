using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts.Models
{
    public class UserLogModel
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public int RequestId { get; set; }
        public DateTime? Date { get; set; }
    }
}
