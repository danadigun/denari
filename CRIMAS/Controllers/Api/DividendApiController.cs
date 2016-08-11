using CRIMAS.Models;
using CRIMAS.Repository.artifacts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CRIMAS.Controllers
{
    /// <summary>
    /// Api controller for dividend management
    /// </summary>
    public class divController : ApiController
    {
        private DividendManagement _ctx;
        private CrimasDb _db;

        public divController()
        {
            _ctx = new DividendManagement();
            _db = new CrimasDb();
        }
      
        [HttpPost]
        public void post(decimal percentage)
        {
            _ctx.postDividend(percentage);
        }

        [HttpGet]
        public IList getDividends(int? dividend_id)
        {
            if (dividend_id.HasValue)
            {
                using (var db = new CrimasDb())
                {
                    return db.DividendSummary.Where(x => x.dividend_id == dividend_id).ToList();
                }
            }
            return _ctx.getSummaries();
        }


        [HttpGet]
        public IList getCustomerDividend(string accountNo)
        {
            return _db.Dividends.Where(x => x.accountNo == accountNo).ToList();
        }
    }
}