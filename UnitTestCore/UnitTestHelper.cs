using Core.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestCore
{
    [TestClass]
    public class UnitTestHelper
    {
        [TestMethod]
        public void TestHMACHelper()
        {
            var h = new HMACHelper("checkuserexists");
            h.AddProperty("puserid", "sploo");
            StringAssert.Equals(
                $"version={h.GetVersion()}&method={h.GetMethod()}&uuid={h.GetMobileUUID()}&trandatetime={h.GetTranDateTime()}&puserid=sploo",
                h.ToString());

            var h1 = new HMACHelper("registernewuser");
            DateTime now = DateTime.Now;
            h1.AddProperty("now", now);

            StringAssert.Equals($"version={h1.GetVersion()}&method={h1.GetMethod()}&uuid={h1.GetMobileUUID()}&trandatetime={h1.GetTranDateTime()}&now={h.GetDateTimeString(now)}",
                h1.ToString());

        }
    }
}
