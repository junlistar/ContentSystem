using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public interface IOrderBusiness
    {
         
        Order Insert(Order model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(Order model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(Order model);

        List<Order> GetDeliveryList(string starttime, string endtime, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<Order> GetManagerList(string orderNo, string mobile, string productname, string sku, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 根据订单编号获取订单和详情列表
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        OrderDetailReturnModel GetOrderDetail(string tid);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<Order> GetAll();
    }
}
