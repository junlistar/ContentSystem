using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public interface IUserInfoBusiness
    {
         
        UserInfo Insert(UserInfo model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(UserInfo model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(UserInfo model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<UserInfo> GetManagerList(string fansid, string openid, string name, int pageNum, int pageSize, out int totalCount);


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<UserInfo> GetAll();
    }
}
