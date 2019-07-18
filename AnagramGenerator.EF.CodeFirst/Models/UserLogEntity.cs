using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Models
{
    public partial class UserLogEntity
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public DateTime? Date { get; set; }
        public int RequestId { get; set; }
        public int UserId { get; set; }

        public virtual RequestEntity Request { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
