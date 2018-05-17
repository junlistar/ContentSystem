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
    public class UserInfoMap : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoMap()
        {
            this.ToTable("UserInfo");
            this.HasKey(m => m.UserInfoId);
            this.Property(m => m.Fans_id);
            this.Property(m => m.UserInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.NickName);
            this.Property(m => m.Fans_id); 
            this.Property(m => m.Fans_weixin_openid);
            this.Property(m => m.Avatar); 
             
        }
    }
}
