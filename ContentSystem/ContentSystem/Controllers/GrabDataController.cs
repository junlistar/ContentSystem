using ContentSystem.Core.Utils;
using ContentSystem.Domain.Model;
using ContentSystem.IService;
using ContentSystem.Models; 
using System.Web.Mvc;

namespace ContentSystem.Controllers
{
    public class GrabDataController : BaseController
    {

        IGrabDataService _grabDataService; 


        public GrabDataController(IGrabDataService grabDataService) {
            _grabDataService = grabDataService;
        }

        /// <summary>
        /// 列表
        /// </summary> 
        /// <returns></returns>
        public ActionResult List(UserInfoVM vm, int pn = 1)
        {
           

            return View(vm);
        }

    }
}