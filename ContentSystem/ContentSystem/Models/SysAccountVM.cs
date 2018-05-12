using ContentSystem.Core.Utils;
using ContentSystem.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContentSystem.Models
{
    public class SysAccountVM
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 实体信息
        /// </summary>
        public SysAccount SysAccount { get; set; }
          
        public List<SysAccount> SysAccountList { get; set; }

        /// <summary>
        /// 用户分页
        /// </summary>
        public Paging<SysAccount> Paging { get; set; }

        public string QueryName { get; set; }
        public string QueryFansId { get; set; }
        public string QueryOpenId { get; set; } 
    }
     
}
