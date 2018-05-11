using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public interface IOrderDetailBusiness
    {
         
        OrderDetail Insert(OrderDetail model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(OrderDetail model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(OrderDetail model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<OrderDetail> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);
         
         
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<OrderDetail> GetAll();
    }
}
