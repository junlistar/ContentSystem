using ContentSystem.Business;
using ContentSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentSystem.Domain.Model;
using System.Collections;
using ContentSystem.Core.Utils;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;

namespace ContentSystem.Service
{
    public class CalendarInfoService : ICalendarInfoService
    {
        /// <summary>
        /// The user biz
        /// </summary>
        private ICalendarInfoBusiness _calendarBiz;

        public CalendarInfoService(ICalendarInfoBusiness calendarBiz)
        {
            _calendarBiz = calendarBiz;
        }

        public CalendarInfo Insert(CalendarInfo model)
        {
            return _calendarBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(CalendarInfo model)
        {
            this._calendarBiz.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(CalendarInfo model)
        {
            this._calendarBiz.Delete(model);
        }

        public bool IsExistName(string name)
        {
            return this._calendarBiz.IsExistName(name);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<CalendarInfo> GetAll()
        {
            return _calendarBiz.GetAll();
        }




        //获取签名url
        string apiUrl = "http://tool.bitefu.net/jiari/";

        /// <summary>
        /// 获取有赞返回的订单列表
        /// </summary>
        /// <param name="year">year</param>
        /// <returns></returns>
        public void InitDays(string year)
        {
            ////存储新抓取的订单中用户的openid
            //var openIdList = new List<string>();
            ////先获取数据库中的抽时间间隔的配置
            //var model = _repoSystemConfig.Table.Where(n => n.Title == "Time").FirstOrDefault();
            //if (model == null)
            //{
            //    model = _repoSystemConfig.Insert(new SystemConfig()
            //    {
            //        Title = "Time",
            //        Val = "30",
            //        Remarks = "时间间隔",
            //    });
            //}
            //int page_no = 1;
            //int page_size = 100;
            //var orderList = new List<YzOrder>();
            var hb = new Hashtable();

            var startTime = DateTime.Parse(year + "-01-01");
            var endTime = startTime.AddYears(1);

            StringBuilder querystr = new StringBuilder("");
            var queryDataStr = "";

            while (startTime < endTime)
            {
                querystr.Append(startTime.ToString("yyyyMMdd") + ",");
                startTime = startTime.AddDays(1);
            }

            if (querystr.Length > 0)
            {
                queryDataStr = querystr.ToString().Substring(0, querystr.Length - 1);
            }

            hb.Add("d", queryDataStr);

           GetForRequest(hb);
           
        }

        public void GetForRequest(Hashtable hb)
        {
            //var orderList = new List<YzOrder>();
            var orderJsonStr = HttpHelp.GetResponseJson(apiUrl, hb);
            //解析返回的json为模型
            //var orderModel = JsonHelper.ParseFormJson<List<Dictionary<string, string>>>(orderJsonStr);

            var orderModel = JsonHelper.JSONStringToList<Dictionary<string, string>>(orderJsonStr);
            //return orderModel; 

            List<CalendarInfo> list = new List<CalendarInfo>();
            List<string> strList = new List<string>();

            JsonReader reader = new JsonTextReader(new StringReader(orderJsonStr));
            while (reader.Read())
            {
                if (reader.Value != null && reader.Value.ToString().Length > 0)
                {
                    strList.Add(reader.Value.ToString());
                }
                Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            };

            Console.WriteLine(strList.Count());


            if (strList.Count() > 0)
            {
                for (int i = 0; i < strList.Count(); i = i + 2)
                {
                    CalendarInfo cinfo = new CalendarInfo();
                    cinfo.Day =int.Parse(strList[i]);
                    cinfo.Status = int.Parse(strList[i + 1]);

                    list.Add(cinfo);
                }


                _calendarBiz.BulkInsert(list);
            }
        }
    }
}
