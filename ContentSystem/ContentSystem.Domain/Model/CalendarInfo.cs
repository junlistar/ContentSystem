using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain.Model
{
    public class CalendarInfo : IAggregateRoot
    {
        public virtual int CalendarInfoId { get; set; }
        public virtual int Day { get; set; }
        public virtual int Status { get; set; } 
          
    }

    public class CalendarResponse {
        public string Days { get; set; }
        public int Status { get; set; }
    }
}
