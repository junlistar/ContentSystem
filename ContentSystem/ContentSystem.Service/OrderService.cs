using ContentSystem.Business;
using ContentSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentSystem.Domain.Model;

namespace ContentSystem.Service
{
    public class OrderService : IOrderService
    {
        /// <summary>
        /// The user biz
        /// </summary>
        private IOrderBusiness _userBiz;

        public OrderService(IOrderBusiness userBiz)
        {
            _userBiz = userBiz;
        }

        public Order Insert(Order model)
        {
            return _userBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Order model)
        {
            this._userBiz.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Order model)
        {
            this._userBiz.Delete(model);
        }

        public List<DeliveryModel> GetDeliveryList(string sendtime, string title, int pageNum, int pageSize, out int totalCount)
        {
            return _userBiz.GetDeliveryList(sendtime, title, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<OrderModel> GetManagerList(string orderNo, string mobile, string productname, string sku, int pageNum, int pageSize, out int totalCount)
        {
            return _userBiz.GetManagerList(orderNo, mobile, productname, sku, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据订单编号获取订单和详情列表
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public OrderDetailReturnModel GetOrderDetail(string tid)
        {
            return _userBiz.GetOrderDetail(tid);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAll()
        {
            return _userBiz.GetAll();
        }

        /// <summary>
        /// 获取发货日期列表
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public List<SendInfo> GetSendInfoList(string tid)
        {
            return _userBiz.GetSendInfoList(tid);
        }
    }
}
