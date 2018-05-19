using ContentSystem.Core.Data;
using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private IRepository<Order> _repoOrder;
        private IRepository<OrderDetail> _repoOrderDetail;
        private IRepository<CalendarInfo> _repoCalendarInfo;
        private IRepository<SendInfo> _repoSendInfo;


        private IRepository<DeliveryModel> _repoDelivery;
        private IRepository<OrderModel> _repoOrderModel;

        


        public OrderBusiness(
          IRepository<Order> repoOrder,
          IRepository<DeliveryModel> repoDelivery,
          IRepository<OrderModel> repoOrderModel,
          IRepository<SendInfo> repoSendInfo,
          IRepository<OrderDetail> repoOrderDetail,
          IRepository<CalendarInfo> repoCalendarInfol
          )
        {
            _repoOrder = repoOrder;
            _repoOrderModel = repoOrderModel;
            _repoDelivery = repoDelivery;
            _repoSendInfo = repoSendInfo;
            _repoOrderDetail = repoOrderDetail;
            _repoCalendarInfo = repoCalendarInfol;
        }
        public Order GetById(int id)
        {
            return this._repoOrder.GetById(id);
        }
        public Order Insert(Order model)
        {
            return this._repoOrder.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Order model)
        {
            this._repoOrder.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Order model)
        {
            this._repoOrder.Delete(model);
        }

        /// <summary>
        /// 获取后续第N个工作日期
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private DateTime GetNextWorkDay(DateTime t1, int count, List<CalendarInfo> clist)
        {

            var _temp = t1;

            while (count > 0)
            {
                _temp = _temp.AddDays(-1);

                var daystr = int.Parse(_temp.ToString("yyyyMMdd"));

                var nday = clist.Where(p => p.Day == daystr).FirstOrDefault();

                if (nday != null)
                {
                    if (nday.Status == 0)
                    {
                        //如果是工作日，计算+1
                        count--;
                    }
                }
                else
                {
                    //如果没有查到，默认为工作日
                    count--;

                }
            }

            //返回计算后的日期
            return _temp;

        }

        public List<DeliveryModel> GetDeliveryList(string sendtime,string title, int pageNum, int pageSize, out int totalCount)
        {
            var calendarlist = _repoCalendarInfo.Table.ToList();

            var where = PredicateBuilder.True<Order>();

            where = where.And(m => m.Pay_time != DateTime.MinValue);

            string whereStr = "";

            // orderNo
            if (!string.IsNullOrEmpty(sendtime))
            { 
                whereStr += " and s.send_time = '" + sendtime + "'";
            }
            if (!string.IsNullOrEmpty(title))
            { 
                whereStr += " and o.title  like '%" + title + "%'";
            }

            string selsql = @"  select s.tid as Tid,o.title as Title,Fetcher_name as UserName,Fetcher_mobile as Phone,s.Send_num as Num,u.Fans_id as FansId,o.Shop_name as ShopName,
 Taboo = STUFF((SELECT ',' + Taboo FROM
OrderDetail WHERE Tid = o.tid FOR XML PATH('')),1,1,'') 
   from [SendInfo] s left join[Order] o left join UserInfo u on o.Fans_weixin_openid = u.Fans_weixin_openid
   on s.tid = o.Tid
   where 1=1 and title like '%包月配送%'  {0} ";

            selsql = string.Format(selsql, whereStr);

            var result = this._repoDelivery.SqlQuery(selsql, new object[] { });


            totalCount = result.Count();
            return result.OrderBy(p => p.UserName).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<OrderModel> GetManagerList(string orderNo, string mobile, string productname, string sku, int pageNum, int pageSize, out int totalCount)
        {
            var whereDetail = PredicateBuilder.True<OrderDetail>();

            if (!string.IsNullOrWhiteSpace(productname))
            {
                whereDetail = whereDetail.And(m => m.Title.Contains(productname));
            }

            if (!string.IsNullOrWhiteSpace(sku))
            {
                whereDetail = whereDetail.And(m => m.sku_id.ToString().Contains(sku));
            }
            var idList = _repoOrderDetail.Table.Where(whereDetail).Select(p => p.Tid).ToList();


            //var where = PredicateBuilder.True<Order>();

            //// orderNo
            //if (!string.IsNullOrEmpty(orderNo))
            //{
            //    where = where.And(m => m.Tid.Contains(orderNo));
            //}
            //if (!string.IsNullOrEmpty(mobile))
            //{
            //    where = where.And(m => m.Receiver_mobile.Contains(mobile));
            //}

            //if (idList.Count > 0)
            //{
            //    where = where.And(m => idList.Contains(m.Tid));
            //}

            //totalCount = this._repoOrder.Table.Where(where).Count();
            //return this._repoOrder.Table.Where(where).OrderByDescending(p => p.Created).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            string whereStr = "";

            string selsql = @"select  OrderId,
Fans_id as Fans_id , a.Tid as Tid, NickName as NickName, a.Fans_weixin_openid as Fans_weixin_openid, Avatar , Fetcher_name , Fetcher_mobile,Title,
sku_id = STUFF((SELECT ',' + CONVERT(NVARCHAR(50), sku_id) FROM
OrderDetail WHERE Tid = a.tid FOR XML PATH('')),1,1,'') , 
Total_fee, Payment, Created , Pay_time, Receiver_address
, (case  Shipping_type 
when 'express' then '快递'  
when 'fetch' then '到店自提'
when 'local' then '同城配送'
else '无'
end) as Shipping_type
, 
Taboo = STUFF((SELECT ',' + Taboo FROM
OrderDetail WHERE Tid = a.tid FOR XML PATH('')),1,1,'') ,   
Status_str ,
Buyer_message,
a.Start_send,
a.End_send,
ISNULL(t.Send_day,0) as Send_day,
ISNULL(t.Send_total,0) as Send_total
from [Order] a
left join UserInfo u on a.Fans_weixin_openid = u.Fans_weixin_openid 
left join (select Tid,COUNT(*) as Send_day,SUM(Send_num) as Send_total from SendInfo 
group by Send_num,Tid) t on a.Tid=t.Tid
where 1=1
 
 {0}";

            // orderNo
            if (!string.IsNullOrEmpty(orderNo))
            {
                whereStr += string.Format(" and a.Tid like '%{0}%'", orderNo);
            }
            if (!string.IsNullOrEmpty(mobile))
            {
                whereStr += string.Format(" and a.Fetcher_mobile like '%{0}%'", mobile); 
            }

            if (!string.IsNullOrWhiteSpace(productname) || !string.IsNullOrWhiteSpace(sku))
            { 
                whereStr += string.Format(" and a.Tid in ('{0}')", string.Join("','", idList));
            }

            selsql = string.Format(selsql, whereStr);

            var result = this._repoOrderModel.SqlQuery(selsql, new object[] { });


            totalCount = result.Count();
            return result.OrderByDescending(p => p.Created).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据订单编号获取订单和详情列表
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public OrderDetailReturnModel GetOrderDetail(string tid)
        {
            OrderDetailReturnModel returnModel = new OrderDetailReturnModel();


            if (!string.IsNullOrWhiteSpace(tid))
            {
                var model = this._repoOrder.Table.Where(t => t.Tid == tid).FirstOrDefault();
                if (model != null)
                {
                    returnModel.Order = model;
                    returnModel.DetailList = this._repoOrderDetail.Table.Where(t => t.Tid == tid).ToList();

                }
            }
            return returnModel;
        }


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAll()
        {
            return this._repoOrder.Table.ToList();
        }

        /// <summary>
        /// 获取发货日期列表
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public List<SendInfo> GetSendInfoList(string tid)
        {
            return this._repoSendInfo.Table.Where(p=>p.Tid == tid).ToList();
        }
    }
}


