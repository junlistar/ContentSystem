using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentSystem.Domain.Model;
using ContentSystem.Core.Infrastructure;
using ContentSystem.IService;

namespace ContentSystem.Business.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IUserInfoBusiness _userInfo = EngineContext.Current.Resolve<IUserInfoBusiness>();
        private readonly IGrabDataService _grabDataService = EngineContext.Current.Resolve<IGrabDataService>();
        private readonly ICalendarInfoService _calendarService = EngineContext.Current.Resolve<ICalendarInfoService>();
        [TestMethod]
        public void AddUserTest()
        {
            Random rd = new Random();


            UserInfo userInfo = new UserInfo();
            userInfo.NickName = "张大胆" + rd.Next(99999);
             

            var addResult = _userInfo.Insert(userInfo);


        }
        [TestMethod]
        public void GetOrder()
        {
            string token = _grabDataService.GetYzToken();
            _grabDataService.GetYzOrder(token);

        }

        /// <summary>
        /// 获取一年的工作日
        /// </summary>
        [TestMethod]
        public void Get365Jiari()
        {
            _calendarService.InitDays("2018");

        }
    }
}
