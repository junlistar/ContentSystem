using ContentSystem.Core.Data;
using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public class OrderDetailBusiness:IOrderDetailBusiness
    {
        private IRepository<OrderDetail> _repoOrderDetail;

        public OrderDetailBusiness(
          IRepository<OrderDetail> repoOrderDetail
          )
        {
            _repoOrderDetail = repoOrderDetail;
        }
        
        public OrderDetail Insert(OrderDetail model)
        {
            return this._repoOrderDetail.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(OrderDetail model)
        {
            this._repoOrderDetail.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(OrderDetail model)
        {
            this._repoOrderDetail.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<OrderDetail> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<OrderDetail>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Title.Contains(name));
            }

            totalCount = this._repoOrderDetail.Table.Where(where).Count();
            return this._repoOrderDetail.Table.Where(where).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

   
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<OrderDetail> GetAll()
        { 
            return this._repoOrderDetail.Table.ToList();
        }
    }
}


