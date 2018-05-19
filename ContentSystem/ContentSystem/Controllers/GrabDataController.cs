using ContentSystem.Common;
using ContentSystem.Core.Utils;
using ContentSystem.Domain.Model;
using ContentSystem.IService;
using ContentSystem.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ContentSystem.Controllers
{
    public class GrabDataController : BaseController
    {

        IGrabDataService _grabDataService;
        ICalendarInfoService _calendarService;


        public GrabDataController(IGrabDataService grabDataService, ICalendarInfoService calendarService)
        {
            _grabDataService = grabDataService;
            _calendarService = calendarService;
        }

        /// <summary>
        /// 列表
        /// </summary> 
        /// <returns></returns>
        public ActionResult List(OrderVM vm, int pn = 1)
        {


            return View(vm);
        }

        [HttpPost]
        public JsonResult GrabOrderData()
        {
            try
            { 
                string token = _grabDataService.GetYzToken();
                _grabDataService.GetYzOrder(token);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult InitYear(int year = 0)
        {
            try
            {
                var yearstr = year.ToString();
                var nexryear = year + 1;

                if (yearstr.Length != 4)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }

                //判断是否存在数据
                var minDay = int.Parse(yearstr + "0000");
                var maxDay = int.Parse(nexryear + "0000");
                var existCount = _calendarService.GetAll().Where(p => p.Day > minDay && p.Day < maxDay).Count();
                if (existCount > 0)
                {
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _calendarService.InitDays(year.ToString());
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}