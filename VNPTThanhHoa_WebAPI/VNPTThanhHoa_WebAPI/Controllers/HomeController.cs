using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;
using VNPTThanhHoa_WebAPI.Models;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VNPTThanhHoa_WebAPI.Controllers
{

    [Route("api/home")]
    public class HomeController : Controller
    {
        // GET: /<controller>/
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       
        [Route("Welcome/{CountMe}")]
        [HttpGet]
        public async Task<CustomerAccountInfoViewModel> Welcome(string CountMe, string Account)
        {
            //dependency inject it in required controller or service class
            // public  IConfiguration Config{ get;}
            //this will get your connection string 

            string linetestUser = "dokiem.tha";
            string linetestPassword = "dokiem.tha";
            string DemoAccount = "ttth376fttx";
            if (!string.IsNullOrEmpty(Account)) DemoAccount = Account;
            CustomerAccountInfoViewModel Customer = new CustomerAccountInfoViewModel() ;
            if (!string.IsNullOrEmpty(CountMe)&&CountMe=="12345678A@a")
            {
                try
                {
                    //ServicesSoapClient service = new ServicesSoapClient();
                    ServicesSoapClient Service = new ServicesSoapClient(ServicesSoapClient.EndpointConfiguration.ServicesSoap);
                    Task<CustomerAccountInfo[]> getaccount = Service.GetAccountAsync(linetestUser, linetestPassword, DemoAccount);
                    //CustomerAccountInfo[] Customer = await Service.GetAccountAsync(linetestUser, linetestPassword, account);
                    //var getnewaccount = Service;
                    var AccountInfo = await getaccount;
                    foreach (var A in AccountInfo)
                    {
                        Customer.Custommeraccountinfo.AddFirst(A);
                    }
                    //CustomerAccountInfo[] Account = await GetAccountAsync(linetestUser, linetestPassword, account);
                    //return Customer;// " This is just welcome";

                }
                catch(Exception ex)
                {
                   
                    
                    Customer.Error = ex.Message.ToString();
                }
                finally
                {

                }
            }

           // List<Int32> TestList = new List<Int32>();
            //TestList.
            return Customer;
        }
    }
}
