using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net;
using EgovaBLL;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private static EgovaLog egovaLog = new EgovaLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [TestMethod]
        public void TestMethod1()
        {
            string str = SecurityEncryption.DESDecrypt("7OuEmmat4nM=");
            string str1 = SecurityEncryption.DESEncrypt("123");
            Console.WriteLine(str);
        }
        [TestMethod]
        public void TestMethod2()
        {
            if(logger.IsDebugEnabled)
            {
                logger.Debug("debug");
            }
            if(logger.IsInfoEnabled)
            {
                logger.Info("info");
            }
            if(logger.IsErrorEnabled)
            {
                logger.Error("error");
            }
            
            
        }
        [TestMethod]
        public void TestMethod3()
        {

            egovaLog.Error("errorr egovalog");


        }
    }
}
