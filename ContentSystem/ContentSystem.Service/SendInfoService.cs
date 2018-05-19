using ContentSystem.Business;
using ContentSystem.IService;
using System;
using System.Collections.Generic; 
using ContentSystem.Domain.Model; 

namespace ContentSystem.Service
{
    public class SendInfoService : ISendInfoService
    {
        /// <summary>
        /// The user biz
        /// </summary>
        private ISendInfoBusiness _sendInfoBiz;

        public SendInfoService(ISendInfoBusiness sendInfoBiz)
        {
            _sendInfoBiz = sendInfoBiz;
        }

        public SendInfo Insert(SendInfo model)
        {
            return _sendInfoBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SendInfo model)
        {
            this._sendInfoBiz.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SendInfo model)
        {
            this._sendInfoBiz.Delete(model);
        }
         

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<SendInfo> GetAll()
        {
            return _sendInfoBiz.GetAll();
        }
         
    }
}
