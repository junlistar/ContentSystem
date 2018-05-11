using ContentSystem.Core.Data;
using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public class SystemConfigBusiness:ISystemConfigBusiness
    {
        private IRepository<SystemConfig> _repoSystemConfig;

        public SystemConfigBusiness(
          IRepository<SystemConfig> repoSystemConfig
          )
        {
            _repoSystemConfig = repoSystemConfig;
        }
        
        public SystemConfig Insert(SystemConfig model)
        {
            return this._repoSystemConfig.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SystemConfig model)
        {
            this._repoSystemConfig.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SystemConfig model)
        {
            this._repoSystemConfig.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SystemConfig> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<SystemConfig>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Title.Contains(name));
            }

            totalCount = this._repoSystemConfig.Table.Where(where).Count();
            return this._repoSystemConfig.Table.Where(where).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
 

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<SystemConfig> GetAll()
        { 
            return this._repoSystemConfig.Table.ToList();
        }
        
    }
}


