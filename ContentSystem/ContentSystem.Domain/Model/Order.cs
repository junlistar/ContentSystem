using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain.Model
{
    public class Order : IAggregateRoot
    { 
        public virtual string Title { get; set; }
        public virtual string Tid { get; set; }
        public virtual decimal Total_fee { get; set; }
        public virtual string Pic_thumb_path { get; set; }
        public virtual string Status_str { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string Fans_weixin_openid { get; set; }
        public virtual decimal Payment { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Buyer_message { get; set; }
        public virtual string Shipping_type { get; set; }
        public virtual string Receiver_state { get; set; }
        public virtual string Receiver_city { get; set; }
        public virtual string Receiver_district { get; set; }
        public virtual string Receiver_address { get; set; }
        public virtual string Receiver_mobile { get; set; }
        public virtual string Fetcher_name { get; set; }
        public virtual string Fetcher_mobile { get; set; }
        public virtual string Fetch_time { get; set; }
        public virtual int Shop_id { get; set; }
        public virtual string Shop_name { get; set; }
        public virtual string Shop_mobile { get; set; }
        public virtual string Shop_state { get; set; }
        public virtual string Shop_city { get; set; }
        public virtual string Shop_district { get; set; }
        public virtual string Shop_address { get; set; } 
         

    }
}
