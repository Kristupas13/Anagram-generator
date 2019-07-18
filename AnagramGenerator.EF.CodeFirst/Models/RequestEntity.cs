using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Models
{
    public partial class RequestEntity
    {
        public RequestEntity()
        {
            UserLogEntity = new HashSet<UserLogEntity>();
            CachedEntity = new HashSet<CachedEntity>();
        }
        public int Id { get; set; }
        public string Word { get; set; }

        public virtual ICollection<UserLogEntity> UserLogEntity { get; set; }
        public virtual ICollection<CachedEntity> CachedEntity { get; set; }
    }
}
