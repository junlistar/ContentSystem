using ContentSystem.Core.Infrastructure;
using ContentSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ContentSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly IGrabDataService _grabDataService = EngineContext.Current.Resolve<IGrabDataService>();
        protected void Application_Start()
        {
            EngineContext.Initialize(false);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Thread t = new Thread(new ThreadStart(StartThread));
            t.Start();
        }
        public void StartThread() {
            while (true)
            {
                string token = _grabDataService.GetYzToken();
                _grabDataService.GetYzOrder(token);
                Thread.Sleep(5000);
            }
           
        }
    }
}
