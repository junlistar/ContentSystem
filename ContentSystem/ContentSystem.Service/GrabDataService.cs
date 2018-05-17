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
        private IRepository<Order> _repoOrder;
        private IRepository<OrderDetail> _repoOrderDetail;
        private IRepository<UserInfo> _repoUserInfo;
        private IRepository<CalendarInfo> _repoCalendarInfo;
        private IRepository<SendInfo> _repoSendInfo;

        public GrabDataService(IRepository<SystemConfig> repoSystemConfig,
            IRepository<Order> repoOrder,
            IRepository<OrderDetail> repoOrderDetail,
            IRepository<UserInfo> repoUserInfo,
            IRepository<CalendarInfo> repoCalendarInfo,
            IRepository<SendInfo> repoSendInfo
            )
        {
            _repoSystemConfig = repoSystemConfig;
            _repoOrder = repoOrder;
            _repoUserInfo = repoUserInfo;
            _repoOrderDetail = repoOrderDetail;
            _repoCalendarInfo = repoCalendarInfo;
            _repoSendInfo = repoSendInfo;
        }
        //获取签名url
        string yzTokenUrl = "https://open.youzan.com/oauth/token";
        string yzOrderUrl = "https://open.youzan.com/api/oauthentry/youzan.trades.sold/3.0.0/get";
        string yzUserUrl = "https://open.youzan.com/api/oauthentry/youzan.users.weixin.follower/3.0.0/get";
        /// <summary>
        /// 获取有赞返回的订单列表
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public void GetYzOrder(string token)
        {
            //存储新抓取的订单中用户的openid
            var openIdList = new List<string>();
            //先获取数据库中的抽时间间隔的配置
            var model = _repoSystemConfig.Table.Where(n => n.Title == "Time").FirstOrDefault();
            if (model == null)
            {
                model = _repoSystemConfig.Insert(new SystemConfig()
                {
                    Title = "Time",
                    Val = "90",
                    Remarks = "时间间隔",
                });
            }
            int page_no = 1;
            int page_size = 100;
            var orderList = new List<YzOrder>();
            var hb = new Hashtable();
            hb.Add("access_token", token);
            hb.Add("start_created", DateTime.Now.AddDays(-int.Parse(model.Val.Trim())));
            hb.Add("end_created", DateTime.Now);
            hb.Add("page_no", page_no);
            hb.Add("page_size", page_size);
            hb.Add("fields", "tid,title,total_fee,pic_thumb_path,status_str,created,payment,buyer_message,shipping_type,receiver_state,receiver_city,receiver_district,receiver_address,receiver_mobile,fetch_detail,orders,fans_info,pay_time");
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
            foreach (YzOrder item in orderList)
            {
                openIdList.Add(item.fans_info != null ? item.fans_info.fans_weixin_openid : "");
                var orderEntity = _repoOrder.Table.Where(m => m.Tid == item.tid).FirstOrDefault();

                //存在
                if (orderEntity != null)
                {
                    var newOrderEntity = VmToEntity(item, orderEntity);

                    if (item.pay_time != null && item.pay_time != "")
                    {
                        //先查询是否存在

                        var count = _repoSendInfo.Table.Where(m => m.Tid == item.tid).Count();
                        if (count == 0)
                        {
                            //添加开始配送时间，结束配送时间，以及配送总天数
                            //开始配送时间，默认为订单支付成功后的一个工作日
                            string payTime = DateTime.Parse(item.pay_time).ToString("yyyyMMdd");
                            var startCalendar = _repoCalendarInfo.Table.Where(m => int.Parse(m.Day) > int.Parse(payTime)
                            && m.Status == 0).OrderBy(m => m.Day).FirstOrDefault();
                            newOrderEntity.Start_send = startCalendar.Day;
                            //结束配送时间，默认为从开始配送时间往后算22个工作日
                            var endCalendar = _repoCalendarInfo.Table.Where(m => int.Parse(m.Day) > int.Parse(payTime)
                            && m.Status == 0).OrderBy(m => m.Day).Take(22).LastOrDefault();
                            newOrderEntity.End_send = endCalendar.Day;
                            newOrderEntity.send_day = 22;
                            //添加配送表记录
                            AddSendInfo(newOrderEntity.Tid, payTime);
                        }
                    }

                    _repoOrder.Update(newOrderEntity);
                    var orderDatail = _repoOrderDetail.Table.Where(m => m.Tid == newOrderEntity.Tid);
                    if (orderDatail.ToList().Count > 0)
                    {
                        _repoOrderDetail.Delete(orderDatail);
                    }
                    foreach (Orders item1 in item.orders)
                    {
                        var wx_no = "";
                        var taboo = "";
                        if (item1.buyer_messages != null && item1.buyer_messages.Count > 0)
                        {
                            foreach (buyer_messages item2 in item1.buyer_messages)
                            {
                                if (item2.title.Equals("微信号"))
                                {
                                    wx_no = item2.content;
                                }
                                if (item2.title.Equals("忌口水果"))
                                {
                                    taboo = item2.content;
                                }
                            }
                        }
                        var orderDatailModel = new OrderDetail
                        {
                            Oid = int.Parse(item1.oid),
                            item_id = item1.item_id,
                            Num = item1.num,
                            Outer_item_id = item1.outer_item_id,
                            Outer_sku_id = item1.outer_sku_id,
                            Price = item1.price,
                            sku_id = item1.sku_id,
                            Tid = item.tid,
                            Title = item1.title,
                            Total_fee = item1.total_fee,
                            Wx_no = wx_no,
                            Taboo = taboo
                        };
                        _repoOrderDetail.Insert(orderDatailModel);
                    }

                }
                else
                {
                    orderEntity = new Order();
                    //不存在
                    var newOrderEntity = VmToEntity(item, orderEntity);
                    if (item.pay_time != null && item.pay_time != "")
                    {
                        //添加开始配送时间，结束配送时间，以及配送总天数
                        //开始配送时间，默认为订单支付成功后的一个工作日
                        string payTime = DateTime.Parse(item.pay_time).ToString("yyyyMMdd");
                        var startCalendar = _repoCalendarInfo.Table.Where(m => int.Parse(m.Day) > int.Parse(payTime)
                        && m.Status == 0).OrderBy(m => m.Day).FirstOrDefault();
                        newOrderEntity.Start_send = startCalendar.Day;
                        //结束配送时间，默认为从开始配送时间往后算22个工作日
                        var endCalendar = _repoCalendarInfo.Table.Where(m => int.Parse(m.Day) > int.Parse(payTime)
                        && m.Status == 0).OrderBy(m => m.Day).Take(22).LastOrDefault();
                        newOrderEntity.End_send = endCalendar.Day;
                        newOrderEntity.send_day = 22;
                        //添加配送表记录
                        AddSendInfo(newOrderEntity.Tid, payTime);

                    }

                    //配送总天数默认为22天。


                    _repoOrder.Insert(newOrderEntity);
                    foreach (Orders item1 in item.orders)
                    {
                        var wx_no = "";
                        var taboo = "";
                        if (item1.buyer_messages != null && item1.buyer_messages.Count > 0)
                        {
                            foreach (buyer_messages item2 in item1.buyer_messages)
                            {
                                if (item2.title.Equals("微信号"))
                                {
                                    wx_no = item2.content;
                                }
                                if (item2.title.Equals("忌口水果"))
                                {
                                    taboo = item2.content;
                                }
                            }
                        }
                        var orderDatailModel = new OrderDetail
                        {
                            Oid = int.Parse(item1.oid),
                            item_id = item1.item_id,
                            Num = item1.num,
                            Outer_item_id = item1.outer_item_id,
                            Outer_sku_id = item1.outer_sku_id,
                            Price = item1.price,
                            sku_id = item1.sku_id,
                            Tid = item.tid,
                            Title = item1.title,
                            Total_fee = item1.total_fee,
                            Wx_no = wx_no,
                            Taboo = taboo

                        };
                        _repoOrderDetail.Insert(orderDatailModel);
                    }
                }
            }
            //这里根据获取到的订单用户openid查找该用户的详细信息
            OrderUserDetail(openIdList, token);
        }

        private void AddSendInfo(string tid, string payTime)
        {
            var calendarList = _repoCalendarInfo.Table.Where(m => int.Parse(m.Day) > int.Parse(payTime)
                        && m.Status == 0).OrderBy(m => m.Day).Take(22);
            foreach (CalendarInfo item in calendarList)
            {
                _repoSendInfo.Insert(new SendInfo()
                {
                    Tid = tid,
                    Is_send = 1,
                    Send_num = 1,
                    Send_time = DateTime.Parse(item.Day).ToString("yyyy-MM-dd")
                });
            }
        }


        /// <summary>
        /// 根据orenid获取该用户的详细信息
        /// </summary>
        /// <param name="openIdList"></param>
        private void OrderUserDetail(List<string> openIdList, string token)
        {
            //list去重复
            openIdList.Distinct();
            foreach (string item in openIdList)
            {
                var hb = new Hashtable();
                hb.Add("access_token", token);
                hb.Add("weixin_openid", item);
                hb.Add("fields", "nick,weixin_openid,avatar,user_id");
                //
                var userJsonStr = HttpHelp.GetResponseJson(yzUserUrl, hb);
                userJsonStr = userJsonStr.Replace(@"{""response"":", "");
                userJsonStr = userJsonStr.Substring(0, userJsonStr.Length - 1);
                userJsonStr = userJsonStr.Replace(@"{""user"":", "");
                userJsonStr = userJsonStr.Substring(0, userJsonStr.Length - 1);
                var userModel = JsonHelper.ParseFormJson<CrmWeixinFans>(userJsonStr);

                if (userModel != null && userModel.weixin_openid != "")
                {
                    var userEntity = _repoUserInfo.Table.Where(m => m.Fans_weixin_openid == userModel.weixin_openid).FirstOrDefault();
                    if (userEntity != null)
                    {
                        userEntity.Avatar = userModel.avatar;
                        userEntity.Fans_id = userModel.user_id.ToString();
                        userEntity.NickName = userModel.nick;
                        userEntity.Fans_weixin_openid = userModel.weixin_openid;
                        _repoUserInfo.Update(userEntity);
                    }
                    else
                    {
                        userEntity = new UserInfo();
                        userEntity.Avatar = userModel.avatar;
                        userEntity.Fans_id = userModel.user_id.ToString();
                        userEntity.NickName = userModel.nick;
                        userEntity.Fans_weixin_openid = userModel.weixin_openid;
                        _repoUserInfo.Insert(userEntity);
                    }
                }

            }
        }
        /// <summary>
        /// 订单VM转换订单Entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="orderEntity"></param>
        /// <returns></returns>
        private static Order VmToEntity(YzOrder item, Order orderEntity)
        {
            orderEntity.Buyer_message = item.buyer_message;
            orderEntity.Created = DateTime.Parse(item.created);
            orderEntity.Fans_weixin_openid = item.fans_info.fans_weixin_openid;
            orderEntity.Fetcher_mobile = item.fetch_detail != null ? item.fetch_detail.fetcher_mobile : "";
            orderEntity.Fetcher_name = item.fetch_detail != null ? item.fetch_detail.fetcher_name : "";
            orderEntity.Fetch_time = item.fetch_detail != null ? item.fetch_detail.fetch_time : "";
            orderEntity.Payment = item.payment;
            orderEntity.Pic_thumb_path = item.pic_thumb_path;
            orderEntity.Receiver_address = item.receiver_address;
            orderEntity.Receiver_city = item.receiver_city;
            orderEntity.Receiver_district = item.receiver_district;
            orderEntity.Receiver_mobile = item.receiver_mobile;
            orderEntity.Receiver_state = item.receiver_state;
            orderEntity.Shipping_type = item.shipping_type;
            orderEntity.Shop_address = item.fetch_detail != null ? item.fetch_detail.shop_address : "";
            orderEntity.Shop_city = item.fetch_detail != null ? item.fetch_detail.shop_city : "";
            orderEntity.Shop_district = item.fetch_detail != null ? item.fetch_detail.shop_district : "";
            orderEntity.Shop_id = item.fetch_detail != null ? item.fetch_detail.shop_id : 0;
            orderEntity.Shop_mobile = item.fetch_detail != null ? item.fetch_detail.shop_mobile : "";
            orderEntity.Shop_name = item.fetch_detail != null ? item.fetch_detail.shop_name : "";
            orderEntity.Shop_state = item.fetch_detail != null ? item.fetch_detail.shop_state : "";
            orderEntity.Status_str = item.status_str;
            orderEntity.Tid = item.tid;
            orderEntity.Title = item.title;
            orderEntity.Pay_time = item.pay_time == "" ? DateTime.MinValue : DateTime.Parse(item.pay_time);
            orderEntity.Total_fee = item.total_fee;
            return orderEntity;
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
            var tokenModel = new TokenEntity();
            if (model != null)
            {
                var dtime = DateTime.Parse(model.Remarks);
                var nowTime = DateTime.Now;
                var ts = nowTime - dtime;
                int days = ts.Days;
                if (days > 5)
                {
                    //重新获取token并更新数据库存储的token
                    tokenModel = GetToken();
                    model.Val = tokenModel.access_token;
                    model.Remarks = DateTime.Now.ToString();
                    _repoSystemConfig.Update(model);
                }
                else
                {
                    tokenModel.access_token = model.Val;
                }
            }
            else
            {
                tokenModel = GetToken();
                //插入数据库
                _repoSystemConfig.Insert(new SystemConfig()
                {
                    Title = "Token",
                    Val = tokenModel.access_token,
                    Remarks = DateTime.Now.ToString(),
                });
            }

            //更新数据库存储的token对象，并返回
            return tokenModel.access_token;
        }

        private TokenEntity GetToken()
        {
            var hb = new Hashtable();
            hb.Add("client_id", "5b0a461d5285038e73");
            hb.Add("client_secret", "2f599e28788df467acfa9f03e5f0815f");
            hb.Add("grant_type", "silent");
            hb.Add("kdt_id", "40553542");
            var toKenJsonStr = HttpHelp.GetResponseJson(yzTokenUrl, hb);
            var tokenModel = JsonHelper.ParseFormJson<TokenEntity>(toKenJsonStr);
            return tokenModel;
        }
    }
}
