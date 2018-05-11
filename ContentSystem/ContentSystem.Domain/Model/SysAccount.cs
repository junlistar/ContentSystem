using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain.Model
{
    public class SysAccount : IAggregateRoot
    {
        public virtual int SysAccountId { get; set; }
        public virtual string Account { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime CreateTime { get; set; } 
         

    }
}
