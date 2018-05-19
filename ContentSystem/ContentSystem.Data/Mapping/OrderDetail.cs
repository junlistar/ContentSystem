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
    public class OrderDetailMap : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailMap()
        {
            this.ToTable("OrderDetail");
            this.HasKey(m => m.OrderDetailId); 
            this.Property(m => m.OrderDetailId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Num);
            this.Property(m => m.Oid); 
            this.Property(m => m.Outer_sku_id);
            this.Property(m => m.Outer_item_id);
            this.Property(m => m.sku_id);
            this.Property(m => m.item_id);
            this.Property(m => m.Price); 
            this.Property(m => m.Tid); 
            this.Property(m => m.Title); 
            this.Property(m => m.Total_fee);
            this.Property(m => m.Wx_no);
            this.Property(m => m.Taboo);

        }
    }
}
