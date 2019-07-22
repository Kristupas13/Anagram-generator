﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts.Models
{
    public class UserLogModel
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public string RequestedWord { get; set; }
        public DateTime? Date { get; set; }
    }
}
