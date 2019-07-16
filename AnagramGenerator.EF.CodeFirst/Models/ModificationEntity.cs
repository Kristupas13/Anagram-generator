using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Models
{
    public partial class ModificationEntity
    {
        public int Id { get; set; }
        public int Counter { get; set; }

        public int UserId { get; set; }

        public virtual UserLogEntity User { get; set; }
    }
}
