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
        ISendInfoService _sendInfoService;
        ICalendarInfoService _calendarInfoService;


        public OrderController(IOrderService orderService,
             IUserInfoService userService, ISendInfoService sendInfoService,
             ICalendarInfoService calendarInfoServic)
        {
            _orderService = orderService;
            _userService = userService;
            _sendInfoService = sendInfoService;
            _calendarInfoService = calendarInfoServic;
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
        public JsonResult Delay(int orderid, string inputstr)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputstr) || orderid <= 0)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }

                //延期、调整天数
                int tmp_day = 0;
                if (int.TryParse(inputstr, out tmp_day))
                {
                    //如果是数字，赠送处理
                    if (tmp_day > 0)
                    {
                        #region 赠送处理

                        //获取订单对象
                        var orderModel = _orderService.GetById(orderid);
                        if (orderModel != null)
                        {
                            //获取订单 最后截止发货日期，用于赠送延期修改
                            int endTime = Convert.ToInt32(DateTime.Parse(orderModel.End_send).ToString("yyyyMMdd"));
                             
                            //修改发货表发货日期数据：新增对应的赠送日期
                            var calendarList = _calendarInfoService.GetAll().Where(m => m.Day > endTime
                       && m.Status == 0).OrderBy(m => m.Day).Take(tmp_day);
                            foreach (CalendarInfo item in calendarList)
                            {
                                _sendInfoService.Insert(new SendInfo()
                                {
                                    Tid = orderModel.Tid,
                                    Is_send = 1,
                                    Send_num = 1,
                                    Send_time = DateTime.ParseExact(item.Day.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd")
                                });
                            } 
                            //修改订单表 发货开始，结束日期，配送天数
                            orderModel.Send_day += tmp_day;

                            var endCalendar = _calendarInfoService.GetAll().Where(m => m.Day > endTime
                        && m.Status == 0).OrderBy(m => m.Day).Skip(tmp_day - 1).Take(1).FirstOrDefault();
                            orderModel.End_send = endCalendar.Day.ToString();

                            _orderService.Update(orderModel);
                        }
                        else
                        {
                            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                        }

                        #endregion

                    }
                    else
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //如果不是数字
                    var tmp_array = inputstr.Split(',');

                    #region 此操作暂时用不到，只做验证数据处理

                    List<DateTime> dtlist = new List<DateTime>();
                    foreach (var item in tmp_array)
                    {
                        dtlist.Add(Convert.ToDateTime(item));
                    }
                    //排序
                    // dtlist.Sort();

                    #endregion

                    #region 延期处理

                    //获取订单对象
                    var orderModel = _orderService.GetById(orderid);
                    if (orderModel != null)
                    {
                        //获取订单 最后截止发货日期，用于赠送延期修改
                        int endTime = Convert.ToInt32(DateTime.Parse(orderModel.End_send).ToString("yyyyMMdd"));
                        //获取该订单的发货日期列表
                        var sendInfoList = _sendInfoService.GetAll().Where(p => p.Tid == orderModel.Tid).ToList();
                          
                        //获取设置不发货的日期对象
                        var editDayList = sendInfoList.Where(p => tmp_array.Contains(p.Send_time));

                        //设置不发货
                        foreach (var item in editDayList)
                        {
                            item.Is_send = 0;
                            _sendInfoService.Update(item);

                            //统计延期天数
                            tmp_day++;
                        }
  
                        //修改发货表发货日期数据：新增对应的赠送日期
                        var calendarList = _calendarInfoService.GetAll().Where(m => m.Day > endTime
                   && m.Status == 0).OrderBy(m => m.Day).Take(tmp_day);
                        foreach (CalendarInfo item in calendarList)
                        {
                            _sendInfoService.Insert(new SendInfo()
                            {
                                Tid = orderModel.Tid,
                                Is_send = 1,
                                Send_num = 1,
                                Send_time = DateTime.ParseExact(item.Day.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd")
                            });
                        }

                        //修改订单对象
                        var endCalendar = _calendarInfoService.GetAll().Where(m => m.Day > endTime
                       && m.Status == 0).OrderBy(m => m.Day).Skip(tmp_day - 1).Take(1).FirstOrDefault();
                        orderModel.End_send = endCalendar.Day.ToString();

                        _orderService.Update(orderModel);


                        #endregion
                    }
                    else
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    } 
                }

                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult SendSave(int sendInfoId, int is_send, int send_num)
        {
            //先获取实体，然后修改
            var model = _sendInfoService.GetById(sendInfoId);
            if (model!=null&&model.SendInfoId!=0)
            {
                model.Send_num = send_num;
                model.Is_send = is_send;
                _sendInfoService.Update(model);
            }

            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
        }
    }
}