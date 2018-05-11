using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain.Model
{
    public class OrderDetail : IAggregateRoot
    { 
        public virtual string Tid { get; set; }
        public virtual int Oid { get; set; }
        public virtual string Outer_sku_id { get; set; } 
        public virtual string Title { get; set; } 
        public virtual decimal Price { get; set; } 
        public virtual decimal Total_fee { get; set; } 
        public virtual DateTime Pay_time { get; set; } 
        public virtual int Num { get; set; }
         
    }
}
