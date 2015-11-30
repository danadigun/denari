using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebMatrix.WebData;

namespace CRIMAS.SupportClasses
{
    public class ExtendedSimpleMembershipProvider : SimpleMembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            if (IsValidEmail(username))
            {
                string actualUsername = base.GetUserNameByEmail(username);
                return base.ValidateUser(actualUsername, password);
            }
            else
            {
                return base.ValidateUser(username, password);
            }
        }
        bool IsValidEmail(string str)
        {
            //return true if str is a valid email
            return Regex.IsMatch(str, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}