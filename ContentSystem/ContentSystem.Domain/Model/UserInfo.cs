using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain.Model
{
    public class UserInfo: IAggregateRoot
    {
        public virtual int UserInfoId { get; set; }
        public virtual string Fans_id { get; set; }
        public virtual string Fans_weixin_openid { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Avatar { get; set; } 
         

    }
}
