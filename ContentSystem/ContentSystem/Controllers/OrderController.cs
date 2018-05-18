using ContentSystem.Common;
using ContentSystem.Core.Utils;
using ContentSystem.Domain.Model;
using ContentSystem.IService;
using ContentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContentSystem.Controllers
{
    public class OrderController : BaseController
    {

        IOrderService _orderService;
        IUserInfoService _userService;


        public OrderController(IOrderService orderService,
             IUserInfoService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        /// <summary>
        /// 列表
        /// </summary> 
        /// <returns></returns>
        public ActionResult List(OrderVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _orderService.GetManagerList(vm.QueryOrderNo, vm.QueryMobile, vm.QueryProductName, vm.QuerySku, pageIndex, pageSize, out totalCount);
            var paging = new Paging<OrderModel>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;

            return View(vm);
        }

        public ActionResult SendInfoList(OrderVM vm, int pn = 1)
        { 
            var list = _orderService.GetSendInfoList(vm.Tid);
           
            vm.SendInfoList = list;

            return View(vm);
        }

        public ActionResult Detail(string tid)
        {
            var detailModel = _orderService.GetOrderDetail(tid);

            detailModel.UserInfoList = _userService.GetAll();

            return View(detailModel);
        }

        public ActionResult ShipDate(string tid)
        {

            return View();
        }

        /// <summary>
        /// 延期或赠送
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="inputstr"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delay(string tid, string inputstr)
        {
            try
            {  
                if (string.IsNullOrWhiteSpace(inputstr) || string.IsNullOrWhiteSpace(tid))
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
                int tmp_day = 0;
                if (int.TryParse(inputstr, out tmp_day))
                {
                    //如果是数字，赠送处理
                    if (tmp_day > 0)
                    {
                        //todo:赠送处理

                    }
                }
                else
                {
                    //如果不是数字
                    var tmp_array = inputstr.Split(',');
                    List<DateTime> dtlist = new List<DateTime>();
                    foreach (var item in tmp_array)
                    {
                        dtlist.Add(Convert.ToDateTime(item));
                    }
                    //排序
                    dtlist.Sort();
                    //todo:延期处理


                }

                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}