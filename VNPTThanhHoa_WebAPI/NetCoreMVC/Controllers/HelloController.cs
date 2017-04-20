using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreMVC.Controllers
{
    public class HelloController : Controller
    {
        // GET: /<controller>/
        public string Index()
        {
            return "This is index";
        }
        public string Welcome()
        {
            return "Welcome To clean start of .net Core";
              
        }
        public string MyNameIsNow(string name,int number)
        {
            string myname = "";
            string now = DateTime.Now.ToString();
            for(int i = 0; i < number; i++) { myname += name + " thứ :" + i; }
            return myname;
        }

    }
}
