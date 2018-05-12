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

        public OrderBusiness(
          IRepository<Order> repoOrder,
          IRepository<OrderDetail> repoOrderDetail
          )
        {
            _repoOrder = repoOrder;
            _repoOrderDetail = repoOrderDetail;
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


