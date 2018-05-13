using ContentSystem.Core.Utils;
using ContentSystem.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContentSystem.Models
{
    public class OrderVM
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 实体信息
        /// </summary>
        public Order Order { get; set; }

        public OrderDetail OrderDetail { get; set; }

        /// <summary>
        /// 实体列表
        /// </summary>
        public List<Order> OrderList { get; set; }
        public List<OrderDetail> DetailList { get; set; }
        public List<UserInfo> UserInfoList { get; set; }

        /// <summary>
        /// 用户分页
        /// </summary>
        public Paging<Order> Paging { get; set; }

       /// <summary>
       /// 配送分页
       /// </summary>
        public Paging<DeliveryModel> DeliveryPaging { get; set; }

        public string QueryName { get; set; }
        public string QueryOrderNo { get; set; }
        public string QueryMobile { get; set; }
        public string QueryProductName { get; set; }
        public string QuerySku { get; set; }
        public string QueryStartTime { get; set; }
        public string QueryEndTime { get; set; }
    }
     
}
