using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRIMAS.SupportClasses;
using CRIMAS.Repository.artifacts;
using CRIMAS.Models;

namespace denari.tests
{
    [TestClass]
    public class DividendManagement_Tests
    {
        [TestMethod]
        public void SendEmail()
        {
            EmailHelper.SendMail("daniyeladigun@yahoo.com", "Unit Test for crmpcs mail service", "Unit test for mail service");
        }
    
        [TestMethod]
        public void notify_test()
        {
           
            new DividendManagement().notify();
        }
    }
}
