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
    public class SystemConfigMap : EntityTypeConfiguration<SystemConfig>
    {
        public SystemConfigMap()
        {
            this.ToTable("SystemConfig");
            this.HasKey(m => m.Title);
            this.Property(m => m.Remarks);
            this.Property(m => m.Title); 
            this.Property(m => m.Val); 
             
        }
    }
}
