using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Models
{
    public partial class UserEntity
    {
        public UserEntity()
        {
            UserLogEntity = new HashSet<UserLogEntity>();
        }
        public int Id { get; set; }
        public string Ip { get; set; }
        public int Counter { get; set; } = 4;

        public virtual ICollection<UserLogEntity> UserLogEntity { get; set; }
    }
}
