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
    public class SendInfoMap : EntityTypeConfiguration<SendInfo>
    {
        public SendInfoMap()
        {
            this.ToTable("SendInfo");
            this.HasKey(m => m.SendInfoId);
            this.Property(m => m.Tid);
            this.Property(m => m.Send_time);
            this.Property(m => m.Is_send);
            this.Property(m => m.Send_num);
        }
    }
}
