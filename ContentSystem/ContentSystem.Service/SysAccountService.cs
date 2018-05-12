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
    public class SysAccountService : ISysAccountService
    {
        /// <summary>
        /// The user biz
        /// </summary>
        private ISysAccountBusiness _userBiz;

        public SysAccountService(ISysAccountBusiness userBiz)
        {
            _userBiz = userBiz;
        }

        public SysAccount GetById(int id) {
            return _userBiz.GetById(id);
        }

        public SysAccount Insert(SysAccount model)
        {
            return _userBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysAccount model)
        {
            this._userBiz.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysAccount model)
        {
            this._userBiz.Delete(model);
        }

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SysAccount> GetManagerList(string fansid, string openid, string name, int pageNum, int pageSize, out int totalCount)
        {
            return _userBiz.GetManagerList(fansid, openid, name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<SysAccount> GetAll()
        {
            return _userBiz.GetAll();
        }

        public SysAccount Login(string accout, string password) {
            return _userBiz.Login(accout,password);
        }
    }
}
