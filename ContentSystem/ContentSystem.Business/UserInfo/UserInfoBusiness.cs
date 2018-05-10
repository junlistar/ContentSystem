using ContentSystem.Core.Data;
using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public class UserInfoBusiness:IUserInfoBusiness
    {
        private IRepository<UserInfo> _repoUserInfo;

        public UserInfoBusiness(
          IRepository<UserInfo> repoUserInfo
          )
        {
            _repoUserInfo = repoUserInfo;
        }
        
        public UserInfo Insert(UserInfo model)
        {
            return this._repoUserInfo.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(UserInfo model)
        {
            this._repoUserInfo.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(UserInfo model)
        {
            this._repoUserInfo.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<UserInfo> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<UserInfo>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.NickName.Contains(name));
            }

            totalCount = this._repoUserInfo.Table.Where(where).Count();
            return this._repoUserInfo.Table.Where(where).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoUserInfo.Table.Any(p => p.NickName == name);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetAll()
        { 
            return this._repoUserInfo.Table.ToList();
        }
    }
}


