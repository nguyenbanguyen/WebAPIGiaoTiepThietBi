using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;
using VNPTThanhHoa_WebAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VNPTThanhHoa_WebAPI.Controllers
{
    [Route("api/home")]
    public class HomeController : Controller
    {
        // GET: /<controller>/
        
        public IActionResult Index()
        {
            return View();
        }
        [Route("Welcome/{account}")]
        public async Task<CustomerAccountInfoViewModel> Welcome(string account)
        {
            string linetestUser= "dokiem.tha";
            string linetestPassword = "dokiem.tha";
            CustomerAccountInfoViewModel Customer = new CustomerAccountInfoViewModel() ;
            if (!string.IsNullOrEmpty(account))
            {
                try
                {
                    //ServicesSoapClient service = new ServicesSoapClient();
                    ServicesSoapClient Service = new ServicesSoapClient(ServicesSoapClient.EndpointConfiguration.ServicesSoap);
                    Task<CustomerAccountInfo[]> getaccount = Service.GetAccountAsync(linetestUser, linetestPassword, account);
                    //CustomerAccountInfo[] Customer = await Service.GetAccountAsync(linetestUser, linetestPassword, account);
                    var AccountInfo = await getaccount;
                    foreach (var item in AccountInfo)
                    {
                        Customer.Custommeraccountinfo.Add(item);
                    }
                    //CustomerAccountInfo[] Account = await GetAccountAsync(linetestUser, linetestPassword, account);
                    //return Customer;// " This is just welcome";
                }
                catch(Exception ex)
                {
                    Customer.Error = ex.ToString();
                }
                finally
                {

                }
            }
            return Customer;
        }
    }
}
