using ContentSystem.Core.Data;
using ContentSystem.Core.Utils;
using ContentSystem.Domain.Model;
using ContentSystem.IService;
using ContentSystem.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentSystem.Domain.PublicEntity;

namespace ContentSystem.Service
{
    public class GrabDataService : IGrabDataService
    {
        private IRepository<SystemConfig> _repoSystemConfig;

        public GrabDataService(IRepository<SystemConfig> repoSystemConfig)
        {
            _repoSystemConfig = repoSystemConfig;
        }
        //获取签名url
        string yzTokenUrl = "https://open.youzan.com/oauth/token";
        string yzOrderUrl = "https://open.youzan.com/api/oauthentry/youzan.trades.sold/3.0.0/get";
        /// <summary>
        /// 获取有赞返回的订单列表
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public string GetYzOrder(string token)
        {
            int page_no = 1;
            int page_size = 100;
            var orderList = new List<YzOrder>();
            var hb = new Hashtable();
            hb.Add("access_token", token);
            hb.Add("start_created", "2018-2-11 00:00:00");
            hb.Add("end_created", "2018-5-11 00:00:00");
            hb.Add("page_no", page_no);
            hb.Add("page_size", page_size);
            hb.Add("fields", "tid,title,total_fee,pic_thumb_path,status_str,created,payment,buyer_message,shipping_type,receiver_state,receiver_city,receiver_district,receiver_address,receiver_mobile,fetch_detail,orders");
            //hb.Add("fields", "fetch_detail");

            ////tid,title,total_fee,pic_thumb_path,status_str,created,payment,buyer_message,shipping_type,receiver_state,receiver_city,receiver_district,receiver_address,receiver_mobile,fetcher_name,fetcher_mobile,fetch_time,shop_id,shop_name,shop_mobile,shop_state,shop_city,shop_district,shop_address
            var orderModel = GetYzOrderForRequest(hb);
            orderList.AddRange(orderModel.trades);
            //判断条数以及调用次数
            var ii = orderModel.total_results / page_size;
            if (orderModel.total_results % page_size != 0)
            {
                ii = ii + 1;
            }
            for (int i = 2; i <= ii; i++)
            {
                hb.Remove("page_no");
                hb.Add("page_no", i);
                var newModel = GetYzOrderForRequest(hb);
                orderList.AddRange(newModel.trades);
            }

            //这里执行插入数据库方法，先判断是否存在，存在则修改，不存在则添加


            return "";
        }

        public response GetYzOrderForRequest(Hashtable hb)
        {
            var orderList = new List<YzOrder>();
            var orderJsonStr = HttpHelp.GetResponseJson(yzOrderUrl, hb);
            orderJsonStr = orderJsonStr.Replace(@"{""response"":", "");
            orderJsonStr = orderJsonStr.Substring(0, orderJsonStr.Length - 1);
            //解析返回的json为模型
            var orderModel = JsonHelper.ParseFormJson<response>(orderJsonStr);
            return orderModel;
        }

        /// <summary>
        /// 获取有赞返回token
        /// </summary>
        /// <returns></returns>
        public string GetYzToken()
        {
            //先获取数据库中的数据
            var model = _repoSystemConfig.Table.Where(n => n.Title == "Token").FirstOrDefault();
            if (model==null)
            {

            }
            var hb = new Hashtable();
            hb.Add("client_id", "5b0a461d5285038e73");
            hb.Add("client_secret", "2f599e28788df467acfa9f03e5f0815f");
            hb.Add("grant_type", "silent");
            hb.Add("kdt_id", "40553542");
            var toKenJsonStr = HttpHelp.GetResponseJson(yzTokenUrl, hb);
            var tokenModel = JsonHelper.ParseFormJson<TokenEntity>(toKenJsonStr);
            //更新数据库存储的token对象，并返回
            return tokenModel.access_token;
        }
    }
}
