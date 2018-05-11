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
    public class UserInfoService : IUserInfoService
    {
        /// <summary>
        /// The user biz
        /// </summary>
        private IUserInfoBusiness _userBiz;

        public UserInfoService(IUserInfoBusiness userBiz)
        {
            _userBiz = userBiz;
        }
         
        public UserInfo Insert(UserInfo model)
        {
            return _userBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(UserInfo model)
        {
            this._userBiz.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(UserInfo model)
        {
            this._userBiz.Delete(model);
        }

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<UserInfo> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return _userBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetAll()
        {
            return _userBiz.GetAll();
        }
    }
}
