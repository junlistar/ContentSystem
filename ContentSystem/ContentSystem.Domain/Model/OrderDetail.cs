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
        public virtual string Outer_item_id { get; set; }
        public virtual int sku_id { get; set; }
        public virtual int item_id { get; set; }
        public virtual string Title { get; set; } 
        public virtual decimal Price { get; set; } 
        public virtual decimal Total_fee { get; set; } 
        
        public virtual int Num { get; set; }
        public virtual string Wx_no { get; set; }
        public virtual string Taboo { get; set; }
         

    }


    public class OrderDetailReturnModel
    {
         public Order Order { get; set; }
         public List<OrderDetail> DetailList { get; set; }

    }
}
