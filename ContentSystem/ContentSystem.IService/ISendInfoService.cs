using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.IService
{
    public interface ISendInfoService
    {

  
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        SendInfo Insert(SendInfo model);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(SendInfo model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(SendInfo model);
         
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<SendInfo> GetAll();
         
        }
}
