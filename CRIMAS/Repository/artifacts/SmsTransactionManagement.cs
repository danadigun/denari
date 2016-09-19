using CRIMAS.com.clickatell.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Repository.artifacts
{
    public class SmsTransactionManagement
    {
        public void sendMessage(string message, string[]numbers)
        {
            var api = new PushServerWS();

            api.sendmsg("", 3623992, "digitalforte", "psalm!!9", numbers, "2347038025189", message, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, 0, null, null, null, 0);
        }
    }
}