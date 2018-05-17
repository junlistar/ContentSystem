using ContentSystem.Business;
using ContentSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentSystem.Domain.Model;
using System.Collections;
using ContentSystem.Core.Utils;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;

namespace ContentSystem.Service
{
    public class CalendarInfoService : ICalendarInfoService
    {
        /// <summary>
        /// The user biz
        /// </summary>
        private ICalendarInfoBusiness _calendarBiz;

        public CalendarInfoService(ICalendarInfoBusiness calendarBiz)
        {
            _calendarBiz = calendarBiz;
        }

        public CalendarInfo Insert(CalendarInfo model)
        {
            return _calendarBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(CalendarInfo model)
        {
            this._calendarBiz.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(CalendarInfo model)
        {
            this._calendarBiz.Delete(model);
        }

        public bool IsExistName(string name)
        {
            return this._calendarBiz.IsExistName(name);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<CalendarInfo> GetAll()
        {
            return _calendarBiz.GetAll();
        }




        //获取签名url
        string apiUrl = "http://tool.bitefu.net/jiari/";

        /// <summary>
        /// 获取有赞返回的订单列表
        /// </summary>
        /// <param name="year">year</param>
        /// <returns></returns>
        public void InitDays(string year)
        {
            ////存储新抓取的订单中用户的openid
            //var openIdList = new List<string>();
            ////先获取数据库中的抽时间间隔的配置
            //var model = _repoSystemConfig.Table.Where(n => n.Title == "Time").FirstOrDefault();
            //if (model == null)
            //{
            //    model = _repoSystemConfig.Insert(new SystemConfig()
            //    {
            //        Title = "Time",
            //        Val = "30",
            //        Remarks = "时间间隔",
            //    });
            //}
            //int page_no = 1;
            //int page_size = 100;
            //var orderList = new List<YzOrder>();
            var hb = new Hashtable();

            var startTime = DateTime.Parse(year + "-01-01");
            var endTime = startTime.AddYears(1);

            StringBuilder querystr = new StringBuilder("");
            var queryDataStr = "";

            while (startTime < endTime)
            {
                querystr.Append(startTime.ToString("yyyyMMdd") + ",");
                startTime = startTime.AddDays(1);
            }

            if (querystr.Length > 0)
            {
                queryDataStr = querystr.ToString().Substring(0, querystr.Length - 1);
            }

            hb.Add("d", queryDataStr);

            ////tid,title,total_fee,pic_thumb_path,status_str,created,payment,buyer_message,shipping_type,receiver_state,receiver_city,receiver_district,receiver_address,receiver_mobile,fetcher_name,fetcher_mobile,fetch_time,shop_id,shop_name,shop_mobile,shop_state,shop_city,shop_district,shop_address
            GetForRequest(hb);
            //var orderModel = GetForRequest(hb);
            //orderList.AddRange(orderModel.trades);
            ////判断条数以及调用次数
            //var ii = orderModel.total_results / page_size;
            //if (orderModel.total_results % page_size != 0)
            //{
            //    ii = ii + 1;
            //}
            //for (int i = 2; i <= ii; i++)
            //{
            //    hb.Remove("page_no");
            //    hb.Add("page_no", i);
            //    var newModel = GetYzOrderForRequest(hb);
            //    orderList.AddRange(newModel.trades);
            //}

            ////这里执行插入数据库方法，先判断是否存在，存在则修改，不存在则添加
            //foreach (YzOrder item in orderList)
            //{
            //    openIdList.Add(item.fans_info != null ? item.fans_info.fans_weixin_openid : "");
            //    var orderEntity = _repoOrder.Table.Where(m => m.Tid == item.tid).FirstOrDefault();

            //    //存在
            //    if (orderEntity != null)
            //    {
            //        var newOrderEntity = VmToEntity(item, orderEntity);

            //        _repoOrder.Update(newOrderEntity);
            //        var orderDatail = _repoOrderDetail.Table.Where(m => m.Tid == newOrderEntity.Tid);
            //        if (orderDatail.ToList().Count > 0)
            //        {
            //            _repoOrderDetail.Delete(orderDatail);
            //        }
            //        foreach (Orders item1 in item.orders)
            //        {
            //            var wx_no = "";
            //            var taboo = "";
            //            if (item1.buyer_messages != null && item1.buyer_messages.Count > 0)
            //            {
            //                foreach (buyer_messages item2 in item1.buyer_messages)
            //                {
            //                    if (item2.title.Equals("微信号"))
            //                    {
            //                        wx_no = item2.content;
            //                    }
            //                    if (item2.title.Equals("忌口水果"))
            //                    {
            //                        taboo = item2.content;
            //                    }
            //                }
            //            }
            //            var orderDatailModel = new OrderDetail
            //            {
            //                Oid = int.Parse(item1.oid),
            //                item_id = item1.item_id,
            //                Num = item1.num,
            //                Outer_item_id = item1.outer_item_id,
            //                Outer_sku_id = item1.outer_sku_id,
            //                Price = item1.price,
            //                sku_id = item1.sku_id,
            //                Tid = item.tid,
            //                Title = item1.title,
            //                Total_fee = item1.total_fee,
            //                Wx_no = wx_no,
            //                Taboo = taboo
            //            };
            //            _repoOrderDetail.Insert(orderDatailModel);
            //        }

            //    }
            //    else
            //    {
            //        orderEntity = new Order();
            //        //不存在
            //        var newOrderEntity = VmToEntity(item, orderEntity);
            //        _repoOrder.Insert(newOrderEntity);
            //        foreach (Orders item1 in item.orders)
            //        {
            //            var wx_no = "";
            //            var taboo = "";
            //            if (item1.buyer_messages != null && item1.buyer_messages.Count > 0)
            //            {
            //                foreach (buyer_messages item2 in item1.buyer_messages)
            //                {
            //                    if (item2.title.Equals("微信号"))
            //                    {
            //                        wx_no = item2.content;
            //                    }
            //                    if (item2.title.Equals("忌口水果"))
            //                    {
            //                        taboo = item2.content;
            //                    }
            //                }
            //            }
            //            var orderDatailModel = new OrderDetail
            //            {
            //                Oid = int.Parse(item1.oid),
            //                item_id = item1.item_id,
            //                Num = item1.num,
            //                Outer_item_id = item1.outer_item_id,
            //                Outer_sku_id = item1.outer_sku_id,
            //                Price = item1.price,
            //                sku_id = item1.sku_id,
            //                Tid = item.tid,
            //                Title = item1.title,
            //                Total_fee = item1.total_fee,
            //                Wx_no = wx_no,
            //                Taboo = taboo

            //            };
            //            _repoOrderDetail.Insert(orderDatailModel);
            //        }
            //    }
            //} 
        }

        public void GetForRequest(Hashtable hb)
        {
            //var orderList = new List<YzOrder>();
            var orderJsonStr = HttpHelp.GetResponseJson(apiUrl, hb);
            //解析返回的json为模型
            //var orderModel = JsonHelper.ParseFormJson<List<Dictionary<string, string>>>(orderJsonStr);

            var orderModel = JsonHelper.JSONStringToList<Dictionary<string, string>>(orderJsonStr);
            //return orderModel; 

            List<CalendarInfo> list = new List<CalendarInfo>();
            List<string> strList = new List<string>();

            JsonReader reader = new JsonTextReader(new StringReader(orderJsonStr));
            while (reader.Read())
            {
                if (reader.Value != null && reader.Value.ToString().Length > 0)
                {
                    strList.Add(reader.Value.ToString());
                }
                Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            };

            Console.WriteLine(strList.Count());


            if (strList.Count() > 0)
            {
                for (int i = 0; i < strList.Count(); i = i + 2)
                {
                    CalendarInfo cinfo = new CalendarInfo();
                    cinfo.Day =int.Parse(strList[i]);
                    cinfo.Status = int.Parse(strList[i + 1]);

                    list.Add(cinfo);
                }


                _calendarBiz.BulkInsert(list);
            }
        }
    }
}
