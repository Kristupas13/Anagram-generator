using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Models
{
    public partial class UserLogEntity
    {
        public UserLogEntity()
        {
            ModificationEntity = new HashSet<ModificationEntity>();
        }
        public int Id { get; set; }
        public string UserIp { get; set; }
        public string SearchedWord { get; set; }
        public DateTime? Date { get; set; }

        public virtual ICollection<ModificationEntity> ModificationEntity { get; set; }

    }
}
