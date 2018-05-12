using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Domain
{
    public class PublicEntity
    {
        /// <summary>
        /// 有赞返回Token实体
        /// </summary>
        public class TokenEntity {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string scope { get; set; }
        }
        public class response {
            public int total_results { get; set; }
            public List<YzOrder> trades { get; set; }
        }
        /// <summary>
        /// 有赞订单
        /// </summary>
        public class YzOrder {
            /// <summary>
            /// 订单标题
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 交易编号
            /// </summary>
            public string tid { get; set; }
            /// <summary>
            /// 总金额
            /// </summary>
            public decimal total_fee { get; set; }
            /// <summary>
            /// 缩略图
            /// </summary>
            public string pic_thumb_path { get; set; }
            /// <summary>
            /// 状态
            /// </summary>
            public string status_str { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public string created { get; set; }
            /// <summary>
            /// 实付金额
            /// </summary>
            public decimal payment { get; set; }
            /// <summary>
            /// 买家附言
            /// </summary>
            public string buyer_message { get; set; }
            /// <summary>
            /// 配送方式，枚举范围：express（快递），fetch（到店自提），local（同城配送）
            /// </summary>
            public string shipping_type { get; set; }
            /// <summary>
            /// 省份
            /// </summary>
            public string receiver_state { get; set; }
            /// <summary>
            /// 城市
            /// </summary>
            public string receiver_city { get; set; }
            /// <summary>
            /// 地区
            /// </summary>
            public string receiver_district { get; set; }
            /// <summary>
            /// 详细地址
            /// </summary>
            public string receiver_address { get; set; }
            /// <summary>
            /// 收货人电话
            /// </summary>
            public string receiver_mobile { get; set; }
            /// <summary>
            /// 自提模型
            /// </summary>
            public TradeFetch fetch_detail { get; set; }
            /// <summary>
            /// 付款时间
            /// </summary>
            public string pay_time { get; set; }
            /// <summary>
            /// 订单详情
            /// </summary>
            public List<Orders> orders { get; set; }
            /// <summary>
            /// 粉丝
            /// </summary>
            public FansInfo fans_info { get; set; }
        }
        /// <summary>
        /// 自提模型
        /// </summary>
        public class TradeFetch {
            /// <summary>
            /// 领取人
            /// </summary>
            public string fetcher_name { get; set; }
            /// <summary>
            /// 领取人的手机号
            /// </summary>
            public string fetcher_mobile { get; set; }
            /// <summary>
            /// 预约的领取时间
            /// </summary>
            public string fetch_time { get; set; }

            /// <summary>
            /// 自提点id
            /// </summary>
            public int shop_id { get; set; }
            /// <summary>
            /// 自提点名称
            /// </summary>
            public string shop_name { get; set; }
            /// <summary>
            /// 自提点联系方式
            /// </summary>
            public string shop_mobile { get; set; }
            /// <summary>
            /// 自提点所在省份
            /// </summary>
            public string shop_state { get; set; }
            /// <summary>
            /// 自提点所在城市
            /// </summary>
            public string shop_city { get; set; }
            /// <summary>
            /// 自提点所在地区
            /// </summary>
            public string shop_district { get; set; }
            /// <summary>
            /// 自提点详细地址
            /// </summary>
            public string shop_address { get; set; }
        }
        /// <summary>
        /// 订单明细
        /// </summary>
        public class Orders
        {
            /// <summary>
            /// 交易编号
            /// </summary>
            public string tid { get; set; }
            /// <summary>
            /// 交易明细编号
            /// </summary>
            public string oid { get; set; }
            /// <summary>
            /// 商家设定的sku
            /// </summary>
            public string outer_sku_id { get; set; }
            /// <summary>
            /// 商家设定的商品货号
            /// </summary>
            public string outer_item_id { get; set; }
            /// <summary>
            /// 有赞的sku
            /// </summary>
            public int sku_id { get; set; }
            /// <summary>
            /// 商品数字编号
            /// </summary>
            public int item_id { get; set; }
            /// <summary>
            /// 商品标题
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 商品价格。精确到2位小数；单位：元
            /// </summary>
            public decimal price { get; set; }
            /// <summary>
            /// 总金额
            /// </summary>
            public decimal total_fee { get; set; }
            
            /// <summary>
            /// 数量
            /// </summary>
            public int num { get; set; }
            /// <summary>
            /// 买家自定义附加留言
            /// </summary>
            public List<buyer_messages> buyer_messages { get; set; }
        }
        /// <summary>
        /// 买家自定义附加留言
        /// </summary>
        public class buyer_messages {
            /// <summary>
            /// 标题
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 内容
            /// </summary>
            public string content { get; set; }
        }
        /// <summary>
        /// 粉丝模型
        /// </summary>
        public class FansInfo {
            public string fans_weixin_openid { get; set; }
        }
        /// <summary>
        /// 订单用户模型
        /// </summary>
        public class CrmWeixinFans
        {
            /// <summary>
            /// 用户id
            /// </summary>
            public Int64 user_id { get; set; }
            /// <summary>
            /// 微信openid
            /// </summary>
            public string weixin_openid { get; set; }

            /// <summary>
            /// 头像地址
            /// </summary>
            public string avatar { get; set; }

            /// <summary>
            /// 昵称
            /// </summary>
            public string nick { get; set; }
        }
    }

    
}
