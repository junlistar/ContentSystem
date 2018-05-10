using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentSystem.Domain.Model;
using ContentSystem.Core.Infrastructure;

namespace ContentSystem.Business.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IUserInfoBusiness _userInfo = EngineContext.Current.Resolve<IUserInfoBusiness>();
       
        [TestMethod]
        public void AddUserTest()
        {
            Random rd = new Random();


            UserInfo userInfo = new UserInfo();
            userInfo.NickName = "张大胆" + rd.Next(99999);
             

            var addResult = _userInfo.Insert(userInfo);


        }
    }
}
