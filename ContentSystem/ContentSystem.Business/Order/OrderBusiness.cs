using ContentSystem.Core.Data;
using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public class OrderBusiness:IOrderBusiness
    {
        private IRepository<Order> _repoOrder;

        public OrderBusiness(
          IRepository<Order> repoOrder
          )
        {
            _repoOrder = repoOrder;
        }
        
        public Order Insert(Order model)
        {
            return this._repoOrder.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Order model)
        {
            this._repoOrder.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Order model)
        {
            this._repoOrder.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Order> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<Order>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Title.Contains(name));
            }

            totalCount = this._repoOrder.Table.Where(where).Count();
            return this._repoOrder.Table.Where(where).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

       
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAll()
        { 
            return this._repoOrder.Table.ToList();
        }
    }
}


