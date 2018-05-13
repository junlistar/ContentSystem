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
    public class DeliveryController : BaseController
    {

        IOrderService _orderService;
        IUserInfoService _userService;


        public DeliveryController(IOrderService orderService,
             IUserInfoService userService) {
            _orderService = orderService;
            _userService = userService;
        }

        /// <summary>
        /// 列表
        /// </summary> 
        /// <returns></returns>
        public ActionResult List(OrderVM vm, int pn = 1)
        {
            if (string.IsNullOrWhiteSpace(vm.QueryStartTime))
            {
                vm.QueryStartTime = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrWhiteSpace(vm.QueryEndTime))
            {
                vm.QueryEndTime = DateTime.Now.ToString("yyyy-MM-dd");
            }

            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _orderService.GetDeliveryList(vm.QueryStartTime, vm.QueryEndTime, pageIndex, pageSize, out totalCount);
            var paging = new Paging<DeliveryModel>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.DeliveryPaging = paging;

            return View(vm);
        }
         
    }
}