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
    public class CalendarInfoMap : EntityTypeConfiguration<CalendarInfo>
    {
        public CalendarInfoMap()
        {
            this.ToTable("CalendarInfo");
            this.HasKey(m => m.CalendarInfoId);
            this.Property(m => m.CalendarInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Day);
            this.Property(m => m.Status);  
             
        }
    }
}
