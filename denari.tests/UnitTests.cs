using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRIMAS.SupportClasses;
using CRIMAS.Repository.artifacts;
using CRIMAS.Models;
using CRIMAS.Controllers;


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

        [TestMethod]
        public void post_dividend()
        {
            new DividendManagement().postDividend(5);

        }

        [TestMethod]
        public void add_userprofile_test()
        {
            new AddUserController().Create(new UserProfile
            {
                Address = "address",
                ConfirmPassword="pass119",
                email="daniel.adigun@digitalforte.ng",
                FirstName="Daniel",
                LastName="Adigun",
                Password="pass119",
                phone="07038025189",
                role="admin",
                UserName="daniel.adigun@digitalforte.ng"
            });
        }
    }
}
