using ContentSystem.Admin.Common;
using ContentSystem.Core.Infrastructure;
using ContentSystem.IService;
using System;
using System.Web.Mvc;

namespace ContentSystem.Infrastructure
{
    /// <summary>
    /// 账号验证
    /// </summary>
    public class AccountAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            int accountId = Loginer.AccountId;
            if (accountId == 0)
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }

            base.OnActionExecuting(filterContext);
        }
         
    }
}