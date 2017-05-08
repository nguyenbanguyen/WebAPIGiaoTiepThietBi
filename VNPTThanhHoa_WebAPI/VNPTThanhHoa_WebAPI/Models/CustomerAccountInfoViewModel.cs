using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceReference1;
namespace VNPTThanhHoa_WebAPI.Models
{
    public class CustomerAccountInfoViewModel 
    {
        public  LinkedList< CustomerAccountInfo> Custommeraccountinfo { get; set; }
        //public virtual  LinkedList<CustomerAccountInfo> LinkedCustomerfortest{ get; set; }
        public string Error { get; set; }
        public CustomerAccountInfoViewModel()
        {
            //LinkedCustomerfortest = null;
            Custommeraccountinfo =new LinkedList<CustomerAccountInfo>();
            Error = "";
        }
        public void Add(CustomerAccountInfo Account)
        {
            Custommeraccountinfo.AddLast(Account);
        }

        //public string Error { get; set; }
        //public CustomerAccountInfoViewModel()
        //{
        //    Error = "";
        //    Type = "";
        //    Account = "";
        //    ClientName = "";
        //    ClientAddress = "";
        //    Phone = "";
        //    ServicePackage = "";
        //    Password = "";
        //    Status = "";
        //    ServiceSpec = "";
        //    UsedDate = "";
        //    VCID = "";
        //    Port = "";
        //    Ip = "";
        //    Mac = "";
        //    PortVisa = "";
        //    SystemId = "";
        //    StbCode = "";
        //    StbIP = "";
        //    LastLogin = "";
        //    MegaAccType = "";
        //    ClientType = "";
        //    ClientEmail = "";
        //    Bras = "";
        //    StaticIp = "";
        //}
        //public CustomerAccountInfoViewModel(CustomerAccountInfo account)
        //{
        //    //Error = "";
        //    Type = account.Type;
        //    Account = account.Account;
        //    ClientName = account.ClientName;
        //    ClientAddress = account.ClientAddress;
        //    Phone = account.Phone;
        //    ServicePackage = account.ServicePackage;
        //    Password = account.Password;
        //    Status = account.Status;
        //    ServiceSpec = account.ServiceSpec;
        //    UsedDate = account.UsedDate;
        //    VCID = account.VCID;
        //    Port = account.Port;
        //    Ip = account.Ip;
        //    Mac = account.Mac;
        //    PortVisa = account.PortVisa;
        //    SystemId = account.SystemId;
        //    StbCode = account.StbCode;
        //    StbIP = account.StbIP;
        //    LastLogin = account.LastLogin;
        //    MegaAccType = account.MegaAccType;
        //    ClientType = account.ClientType;
        //    ClientEmail = account.ClientEmail;
        //    Bras = account.Bras;
        //    StaticIp = account.StaticIp;
        //}

    }
}
