using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain.Model
{
    public class Order : IAggregateRoot
    {
        public virtual int OrderId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Tid { get; set; }
        public virtual decimal Total_fee { get; set; }
        public virtual string Pic_thumb_path { get; set; }
        public virtual DateTime Pay_time { get; set; }
        public virtual string Status_str { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string Fans_weixin_openid { get; set; }
        public virtual decimal Payment { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Buyer_message { get; set; }
        public virtual string Shipping_type { get; set; }
        public virtual string Receiver_state { get; set; }
        public virtual string Receiver_city { get; set; }
        public virtual string Receiver_district { get; set; }
        public virtual string Receiver_address { get; set; }
        public virtual string Receiver_mobile { get; set; }
        public virtual string Fetcher_name { get; set; }
        public virtual string Fetcher_mobile { get; set; }
        public virtual string Fetch_time { get; set; }
        public virtual int Shop_id { get; set; }
        public virtual string Shop_name { get; set; }
        public virtual string Shop_mobile { get; set; }
        public virtual string Shop_state { get; set; }
        public virtual string Shop_city { get; set; }
        public virtual string Shop_district { get; set; }
        public virtual string Shop_address { get; set; }
        public virtual string Start_send { get; set; }
        public virtual string End_send { get; set; }
        public virtual int Send_day { get; set; }


    }

    public class DeliveryModel : IAggregateRoot
    {
        public string Tid { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public int Num { get; set; }
        public string Sign { get; set; }
        public string FansId { get; set; }
        public string ShopName { get; set; }
        public string Taboo { get; set; }


    }



    /// <summary>
    /// 订单列表数据查询返回模型定义
    /// </summary>
    public class OrderModel : IAggregateRoot
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string Fans_id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string Tid { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string Fans_weixin_openid { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Fetcher_name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Fetcher_mobile { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 商品sku
        /// </summary>
        public string sku_id { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Total_fee { get; set; }
        /// <summary>
        /// 实际支付
        /// </summary>
        public decimal Payment { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime Pay_time { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string Receiver_address { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public string Shipping_type { get; set; }

        /// <summary>
        /// 忌口水果
        /// </summary>
        public string Taboo { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status_str { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Buyer_message { get; set; }
        /// <summary>
        /// 开始配送日期
        /// </summary>
        public  string Start_send { get; set; }
        /// <summary>
        /// 结束配送日期
        /// </summary>
        public  string End_send { get; set; }
        /// <summary>
        /// 配送总天数
        /// </summary>
        public  int Send_day { get; set; }
        /// <summary>
        /// 配送总份数
        /// </summary>
        public int Send_total { get; set; }
    }
}
