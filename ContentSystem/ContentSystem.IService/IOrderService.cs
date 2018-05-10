using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.IService
{
    public interface IOrderService
    {

  
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<Order> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<Order> GetAll();

        }
}
