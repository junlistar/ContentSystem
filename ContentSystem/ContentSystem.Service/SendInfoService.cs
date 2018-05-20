using ContentSystem.Business;
using ContentSystem.IService;
using System;
using System.Collections.Generic;
using ContentSystem.Domain.Model;
using ContentSystem.Core.Data;

namespace ContentSystem.Service
{
    public class SendInfoService : ISendInfoService
    {
        /// <summary>
        /// The user biz
        /// </summary>
        private ISendInfoBusiness _sendInfoBiz;

        private IRepository<SendInfo> _repoSendInfo;

        public SendInfoService(ISendInfoBusiness sendInfoBiz, IRepository<SendInfo> repoSendInfo)
        {
            _sendInfoBiz = sendInfoBiz;
            _repoSendInfo = repoSendInfo;
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

        public SendInfo GetById(int id)
        {
            return _repoSendInfo.GetById(id);
        }
    }
}
