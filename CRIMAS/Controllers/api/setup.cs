using CRIMAS.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CRIMAS.Controllers.api
{
    public class setupController : ApiController
    {
        private DenariDb _context;
        private RestClient _paystackClient;

        public setupController()
        {
            _context = new DenariDb();
            _paystackClient = new RestClient { BaseUrl = "https://api.paystack.co/" };

        }

        [HttpPost]
        public bool registerCustomer(DenariCustomer customer, int subscription_type, string transactionId)
        {
            if (customer != null)
            {
                //check if the customer email exists
                var _email = _context.DenariCustomers.Where(x => x.email == customer.email).FirstOrDefault();

                if (_email != null)
                {
                    return false;
                }
                else
                {
                    //save customer
                    customer.hasPayed = false;
                    _context.DenariCustomers.Add(customer);
                    _context.SaveChanges();

                    //save customer transaction
                    var transaction = new CustomerTransaction();

                    transaction.customer = customer;
                    transaction.transactionId = transactionId;
                    transaction.amount = subscription_type;

                    if (subscription_type == 2525000)
                    {
                        transaction.subscription_type = "Bronze Subscription";
                        _context.CustomerTransactions.Add(transaction);
                        _context.SaveChanges();

                    }
                    if (subscription_type == 3225000)
                    {
                        transaction.subscription_type = "Silver subscription";
                        _context.CustomerTransactions.Add(transaction);
                        _context.SaveChanges();

                    }
                    if (subscription_type == 4225000)
                    {
                        transaction.subscription_type = "Gold subscription";
                        _context.CustomerTransactions.Add(transaction);
                        _context.SaveChanges();

                    }




                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        public void paySuccess(string transactionId)
        {
            var customer =  _context.CustomerTransactions.SingleOrDefault(x => x.transactionId == transactionId).customer;

            if(customer != null)
            {
                customer.hasPayed = true;
                _context.SaveChanges();
            }
           
        }

        [HttpPost]
        public bool requestDemo(DenariCustomer customer)
        {
            if (customer != null)
            {
                //check if the customer email exists
                var _email = _context.DenariCustomers.Where(x => x.email == customer.email).FirstOrDefault();

                if (_email != null)
                {
                    return false;
                }
                else
                {
                    //save customer
                    customer.hasPayed = false;
                    customer.dateCreated = DateTime.Now;
                    _context.DenariCustomers.Add(customer);
                    _context.SaveChanges();
                
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        public string requestLive(int Subscription_type)
        {
           
            switch (Subscription_type)
            {
                case 1:
                    return "https://paystack.com/pay/FBJP9E9v2u";
                    //break;
                case 2:
                    return "https://paystack.com/pay/PCRqFJNMtX";
                    //break;
                case 3:
                    return "https://paystack.com/pay/OeFyhM1cXI";
                    //break;
                case 4:
                    return "https://paystack.com/pay/E0FzPMXEsZ";
                    //break;
                case 5:
                    return "https://paystack.com/pay/9BrAZODdRy";
                    //break;
                case 6:
                    return "https://paystack.com/pay/HjomsKiYYv";
                    //break;
                case 7:
                    return "https://paystack.com/pay/VSLesw1GwJ";
                    //break;
                case 8:
                    return "https://paystack.com/pay/Pqa8uFqrfV";
                    //break;
                case 9:
                    return "https://paystack.com/pay/R0EB3mfDRs";
                    //break;
                default:
                    return null;
                    //break;
            }
        }
    }
}