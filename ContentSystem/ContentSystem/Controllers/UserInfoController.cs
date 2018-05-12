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
    public class UserInfoController : Controller
    {

        IOrderService _orderService;
        IUserInfoService _userService;


        public UserInfoController(IOrderService orderService,
             IUserInfoService userService) {
            _orderService = orderService;
            _userService = userService;
        }

        /// <summary>
        /// 列表
        /// </summary> 
        /// <returns></returns>
        public ActionResult List(UserInfoVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _userService.GetManagerList(vm.QueryFansId, vm.QueryOpenId, vm.QueryName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<UserInfo>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;

            return View(vm);
        }

    }
}