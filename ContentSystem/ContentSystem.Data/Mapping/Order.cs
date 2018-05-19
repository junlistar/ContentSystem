using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentSystem.Domain;
using System.Data.Entity.ModelConfiguration;
using ContentSystem.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema; 

namespace ContentSystem.Data.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            this.ToTable("Order");
            this.HasKey(m => m.OrderId); 
            this.Property(m => m.OrderId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Buyer_message);
            this.Property(m => m.Created);
            this.Property(m => m.Fans_weixin_openid);
            this.Property(m => m.Fetcher_mobile);
            this.Property(m => m.Fetcher_name);
            this.Property(m => m.Fetch_time);
            this.Property(m => m.Payment);
            this.Property(m => m.Pic_thumb_path);
            this.Property(m => m.Receiver_address);
            this.Property(m => m.Pay_time);
            this.Property(m => m.Receiver_city);
            this.Property(m => m.Receiver_district);
            this.Property(m => m.Receiver_mobile);
            this.Property(m => m.Receiver_state);
            this.Property(m => m.Remarks);
            this.Property(m => m.Shipping_type);
            this.Property(m => m.Shop_address);
            this.Property(m => m.Shop_city);
            this.Property(m => m.Shop_district);
            this.Property(m => m.Shop_id);
            this.Property(m => m.Shop_mobile);
            this.Property(m => m.Shop_name);
            this.Property(m => m.Shop_state);
            this.Property(m => m.Status_str);
            this.Property(m => m.Tid);
            this.Property(m => m.Title);
            this.Property(m => m.Total_fee);
            this.Property(m => m.Start_send);
            this.Property(m => m.End_send);
            this.Property(m => m.Send_day);
             

    }
    }
}
