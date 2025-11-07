using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Constant
    {
        public static class Config
        {
            public const string DomainName = "Factura";
            public const string CompanyName = "Factura Fund";
            public const string SupportName = "Gopinath Pazhanivel";
            public const string SupportMail = "facturafund@gmail.com";
            public const string SupportMobile = "+91 7092661353";
        }
        public static class SP
        {
            public const string ErrorLog = "ErrorLog_I";
            public const string Company_CRUD = "Company_CRUD";
            public const string CustomerWizardCreation = "CustomerWizardCreation";
            public const string GetCustomerDetails = "GetCustomerDetails";
        }

        public static class API
        {
            public static class AccountService
            {
                public const string ErrorLog = "api/ErrorLog";
            }
            public static class AdminService
            {

            }
            public static class CustomerService
            {
                public const string CustomerInsert = "api/CustomerInsert";
                public const string GetAllCustomer = "api/GetAllCustomer";
            }
        }
    }
}
