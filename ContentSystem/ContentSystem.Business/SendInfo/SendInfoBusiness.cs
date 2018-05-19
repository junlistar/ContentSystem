using ContentSystem.Core.Data;
using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public class SendInfoBusiness:ISendInfoBusiness
    {
        private IRepository<SendInfo> _repoSendInfo;

        public SendInfoBusiness(
          IRepository<SendInfo> repoSendInfo
          )
        {
            _repoSendInfo = repoSendInfo;
        }
        
        public SendInfo Insert(SendInfo model)
        {
            return this._repoSendInfo.Insert(model);
        }
        public void BulkInsert(List<SendInfo> list)
        {
            this._repoSendInfo.Insert(list);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SendInfo model)
        {
            this._repoSendInfo.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SendInfo model)
        {
            this._repoSendInfo.Delete(model);
        }
        

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<SendInfo> GetAll()
        { 
            return this._repoSendInfo.Table.ToList();
        }
    }
}


