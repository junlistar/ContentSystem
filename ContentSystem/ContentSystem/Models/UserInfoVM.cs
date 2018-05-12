using ContentSystem.Core.Utils;
using ContentSystem.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContentSystem.Models
{
    public class UserInfoVM
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 实体信息
        /// </summary>
        public UserInfo UserInfo { get; set; }
          
        public List<UserInfo> UserInfoList { get; set; }

        /// <summary>
        /// 用户分页
        /// </summary>
        public Paging<UserInfo> Paging { get; set; }

        public string QueryName { get; set; }
        public string QueryFansId { get; set; }
        public string QueryOpenId { get; set; } 
    }
     
}
