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
        [TestMethod]
        public void AddUserTest()
        {
            Random rd = new Random();


            UserInfo userInfo = new UserInfo();
            userInfo.NickName = "张大胆" + rd.Next(99999);
            userInfo.Gender = rd.Next(100) > 50 ? 1 : 0;
            userInfo.IsEnable = 1;
            userInfo.CTime = DateTime.Now;

            var addResult = _userInfo.Insert(userInfo);


        }
        [TestMethod]
        public void GetOrder()
        {
            string token = _grabDataService.GetYzToken();
            _grabDataService.GetYzOrder(token);

        }
    }
}
