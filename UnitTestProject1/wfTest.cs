using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EgovaBLL;
using log4net;
using System.Globalization;

namespace UnitTestProject1
{
    [TestClass]
    public class WFTest
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [TestMethod]
        public void TestMethod1()
        {
            WorkFlowApp wf = new WorkFlowApp();
            string ferid = "c95beb23-0fa8-4a77-8327-8540a0e90c4d";
            string userID = "8a1511ca-6d97-4391-974d-ea3f28be651d";
            int userLevel = 1;
            string idea = "同意";
            bool a = wf.Accept(ferid, userID, userLevel, idea);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string s = DateTime.Now.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            logger.Info("info");
            logger.Error("cese");
        }
        [TestMethod]
        public void TestMethod3()
        {
            WorkFlowApp wf = new WorkFlowApp();
            bool a = wf.ValidateNewBiz("b6444ad4-8a94-4dcc-a88c-8cb51f801b1c",11221);
        }
    }
}
