using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public interface ISendInfoBusiness
    {
         
        SendInfo Insert(SendInfo model);

        void BulkInsert(List<SendInfo> list);

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
