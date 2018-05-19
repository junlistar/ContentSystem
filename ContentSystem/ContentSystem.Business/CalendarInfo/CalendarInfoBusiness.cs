using ContentSystem.Core.Data;
using ContentSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Business
{
    public class CalendarInfoBusiness:ICalendarInfoBusiness
    {
        private IRepository<CalendarInfo> _repoCalendarInfo;

        public CalendarInfoBusiness(
          IRepository<CalendarInfo> repoCalendarInfo
          )
        {
            _repoCalendarInfo = repoCalendarInfo;
        }
        
        public CalendarInfo Insert(CalendarInfo model)
        {
            return this._repoCalendarInfo.Insert(model);
        }
        public void BulkInsert(List<CalendarInfo> list)
        {
            this._repoCalendarInfo.Insert(list);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(CalendarInfo model)
        {
            this._repoCalendarInfo.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(CalendarInfo model)
        {
            this._repoCalendarInfo.Delete(model);
        }
         

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoCalendarInfo.Table.Any(p => p.Day.ToString() == name);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<CalendarInfo> GetAll()
        { 
            return this._repoCalendarInfo.Table.ToList();
        }
    }
}


