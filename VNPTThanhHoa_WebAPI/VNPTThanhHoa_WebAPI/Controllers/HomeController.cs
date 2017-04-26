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
            var sv = new ServicesSoapClient(ServicesSoapClient.EndpointConfiguration.ServicesSoap);
            var x = await sv.GetAccountAsync(linetestUser, linetestPassword, account);

            //CustomerAccountInfo[] Account = await GetAccountAsync(linetestUser, linetestPassword, account);
            return x.ToString();// " This is just welcome";
        }
    }
}
