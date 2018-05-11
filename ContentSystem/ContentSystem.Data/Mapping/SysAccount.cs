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
    public class SysAccountMap : EntityTypeConfiguration<SysAccount>
    {
        public SysAccountMap()
        {
            this.ToTable("SysAccount");
            this.HasKey(m => m.SysAccountId);
            this.Property(m => m.SysAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Account);
            this.Property(m => m.CreateTime); 
            this.Property(m => m.Name);
            this.Property(m => m.Password); 
             
        }
    }
}
