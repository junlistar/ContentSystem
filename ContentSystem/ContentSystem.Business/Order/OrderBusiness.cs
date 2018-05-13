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


        private IRepository<DeliveryModel> _repoDelivery;
        

        public OrderBusiness(
          IRepository<Order> repoOrder,
          IRepository<DeliveryModel> repoDelivery,
          IRepository<OrderDetail> repoOrderDetail,
          IRepository<CalendarInfo> repoCalendarInfol
          )
        {
            _repoOrder = repoOrder;
            _repoDelivery = repoDelivery;
            _repoOrderDetail = repoOrderDetail;
            _repoCalendarInfo = repoCalendarInfol;
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

                var daystr = _temp.ToString("yyyyMMdd");

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

        public List<DeliveryModel> GetDeliveryList(string starttime, string endtime, int pageNum, int pageSize, out int totalCount)
        {
            var calendarlist = _repoCalendarInfo.Table.ToList();

            var where = PredicateBuilder.True<Order>();

            where = where.And(m => m.Pay_time != DateTime.MinValue);

            string whereStr = "";

            // orderNo
            if (!string.IsNullOrEmpty(starttime))
            {
                var t1 = DateTime.Parse(starttime + " 00:00:00");

                var maxDate = GetNextWorkDay(t1, 1, calendarlist);
                maxDate = maxDate.AddDays(1).AddSeconds(-1);

                var minDate = GetNextWorkDay(t1, 22, calendarlist);
                // where = where.And(m => m.Pay_time >= minDate && m.Pay_time <= maxDate);
                whereStr += " and Pay_time>='" + minDate + "' and Pay_time<='" + maxDate+"'";
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                var t1 = DateTime.Parse(endtime + " 00:00:00");

                var maxDate = GetNextWorkDay(t1, 1, calendarlist);
                maxDate = maxDate.AddDays(1).AddSeconds(-1);

                var minDate = GetNextWorkDay(t1, 22, calendarlist);
                // where = where.And(m => m.Pay_time >= minDate && m.Pay_time <= maxDate);
                whereStr += " and Pay_time>='" + minDate + "' and Pay_time<='" + maxDate + "'";

                //使用这种操作表数据字段的函数调用，会提示函数找不到，因此发现操作变量对象
                // where = where.And(m => GetNextWorkDay(m.Pay_time, 1, calendarlist) <= t1 && t1 <= GetNextWorkDay(m.Pay_time, 22, calendarlist));
            }

            //totalCount = this._repoOrder.Table.Where(where).Count();
            //return this._repoOrder.Table.Where(where).OrderByDescending(p => p.Created).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            string selsql = "select Fetcher_name as UserName,Fetcher_mobile as Phone,COUNT(1) as Num FROM [Order] where Pay_time>'1900-01-01 00:00:00.000' and title like '%包月配送%' {0} group by Fetcher_name,Fetcher_mobile";

            selsql = string.Format(selsql, whereStr);

            var result = this._repoDelivery.SqlQuery(selsql, new object[] { });


            totalCount = result.Count();
            return result.OrderBy(p => p.UserName).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Order> GetManagerList(string orderNo, string mobile, string productname, string sku, int pageNum, int pageSize, out int totalCount)
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


            var where = PredicateBuilder.True<Order>();

            // orderNo
            if (!string.IsNullOrEmpty(orderNo))
            {
                where = where.And(m => m.Tid.Contains(orderNo));
            }
            if (!string.IsNullOrEmpty(mobile))
            {
                where = where.And(m => m.Receiver_mobile.Contains(mobile));
            }

            if (idList.Count > 0)
            {
                where = where.And(m => idList.Contains(m.Tid));
            }

            totalCount = this._repoOrder.Table.Where(where).Count();
            return this._repoOrder.Table.Where(where).OrderByDescending(p => p.Created).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
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
    }
}


