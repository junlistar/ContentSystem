using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain.Model
{
    public class SendInfo : IAggregateRoot
    {
        public virtual int SendInfoId { get; set; }
        public virtual string Tid { get; set; }
        public virtual string Send_time { get; set; }
        public virtual int Is_send { get; set; }
        public virtual int Send_num { get; set; }
    }
}
