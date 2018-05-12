using ContentSystem.Core.Data;
using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public class SysAccountBusiness:ISysAccountBusiness
    {
        private IRepository<SysAccount> _repoSysAccount;

        public SysAccountBusiness(
          IRepository<SysAccount> repoSysAccount
          )
        {
            _repoSysAccount = repoSysAccount;
        }
        public SysAccount GetById(int id)
        {
            return this._repoSysAccount.GetById(id);
        }


        public SysAccount Insert(SysAccount model)
        {
            return this._repoSysAccount.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysAccount model)
        {
            this._repoSysAccount.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysAccount model)
        {
            this._repoSysAccount.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SysAccount> GetManagerList(string fansid, string openid, string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<SysAccount>();

             

            totalCount = this._repoSysAccount.Table.Where(where).Count();
            return this._repoSysAccount.Table.Where(where).OrderByDescending(p=>p.SysAccountId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoSysAccount.Table.Any(p => p.Name == name);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<SysAccount> GetAll()
        { 
            return this._repoSysAccount.Table.ToList();
        }

        public SysAccount Login(string accout,string password)
        {
            return this._repoSysAccount.Table.Where(p => p.Account == accout && p.Password == password).FirstOrDefault();
        }

    }
}


