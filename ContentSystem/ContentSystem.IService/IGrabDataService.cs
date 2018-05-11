using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.IService
{
    public interface IGrabDataService
    {
        /// <summary>
        /// 获取有赞返回的订单
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        void GetYzOrder(string token);
        /// <summary>
        /// 获取有赞返回的token
        /// </summary>
        /// <returns></returns>
        string GetYzToken();
    }
}
