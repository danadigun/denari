using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace CRIMAS.Services
{
    public class Currency : ICurrency
    {
        public void SetCurrencyToNaira()
        {
            //Set Culture info for currency
            CultureInfo cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.CurrencySymbol = "₦";

            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }
       
    }
}