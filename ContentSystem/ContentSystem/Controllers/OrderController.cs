﻿using ContentSystem.Core.Utils;
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
    public class OrderController : Controller
    {

        IOrderService _orderService;


        public OrderController(IOrderService orderService) {
            _orderService = orderService;
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
            var list = _orderService.GetManagerList(vm.QueryOrderNo,vm.QueryMobile,vm.QueryProductName,vm.QuerySku, pageIndex, pageSize, out totalCount);
            var paging = new Paging<Order>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;
            return View(vm);
        }

        public ActionResult Detail(string tid)
        {
            var detailModel = _orderService.GetOrderDetail(tid);

            return View(detailModel);
        }
    }
}