using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.IService
{
    public interface ICalendarInfoService
    {

  
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        CalendarInfo Insert(CalendarInfo model);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(CalendarInfo model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(CalendarInfo model);

        bool IsExistName(string name); 

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<CalendarInfo> GetAll();

        void InitDays(string year);
        }
}
