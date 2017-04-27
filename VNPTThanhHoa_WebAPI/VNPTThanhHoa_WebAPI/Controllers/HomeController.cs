using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VNPTThanhHoa_WebAPI.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
    
        public IActionResult Index()
        {
            return View();
        }
        public async Task<string> Welcome()
        {
            string linetestUser= "dokiem.tha";
            string linetestPassword = "dokiem.tha";
            string account = "dokiem.tha";
            //ServicesSoapClient service = new ServicesSoapClient();
            ServicesSoapClient Service = new ServicesSoapClient(ServicesSoapClient.EndpointConfiguration.ServicesSoap);
            Task<CustomerAccountInfo[]> getaccount = Service.GetAccountAsync(linetestUser, linetestPassword, account);
            //CustomerAccountInfo[] Customer = await Service.GetAccountAsync(linetestUser, linetestPassword, account);
            CustomerAccountInfo[] Customer = await getaccount;

            //CustomerAccountInfo[] Account = await GetAccountAsync(linetestUser, linetestPassword, account);
            return Customer.ToString();// " This is just welcome";
        }
    }
}
