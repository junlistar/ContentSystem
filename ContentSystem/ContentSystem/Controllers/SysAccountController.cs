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
    public class SysAccountController : Controller
    {
         
        ISysAccountService _accoutService;


        public SysAccountController(ISysAccountService accoutService) {
            _accoutService = accoutService;
        }

        /// <summary>
        /// 列表
        /// </summary> 
        /// <returns></returns>
        public ActionResult List(SysAccountVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _accoutService.GetManagerList(vm.QueryFansId, vm.QueryOpenId, vm.QueryName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<SysAccount>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;

            return View(vm);
        }


        /// 编辑
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ActionResult Edit(SysAccountVM vm)
        {
            vm.SysAccount = _accoutService.GetById(vm.Id) ?? new SysAccount(); 
            return View(vm);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(SysAccount model)
        {
            try
            {
                if (model.SysAccountId > 0)
                {
                    var entity = _accoutService.GetById(model.SysAccountId);
                    //修改  
                    entity.Name = model.Name;
                    _accoutService.Update(entity);
                }
                else
                { 
                    //添加 
                    model.CreateTime = DateTime.Now;

                    _accoutService.Insert(model);
                }
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int id = 0)
        {
            try
            {
                var entity = _accoutService.GetById(id);

                if (entity == null)
                {
                    return Json(new { Status = Successed.Empty }, JsonRequestBehavior.AllowGet);
                }

                _accoutService.Delete(entity);

                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}