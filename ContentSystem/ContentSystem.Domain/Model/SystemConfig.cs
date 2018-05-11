using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain.Model
{
    public class SystemConfig : IAggregateRoot
    { 
        public virtual string Title { get; set; }
        public virtual string Val { get; set; }
        public virtual string Remarks { get; set; } 
         

    }
}
