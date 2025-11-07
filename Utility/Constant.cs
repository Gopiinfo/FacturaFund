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
            public const string DomainName = "CMS";
            public const string CompanyName = "Sree Agency";
            public const string SupportName = "Karthi";
            public const string SupportMail = "karthi@gmail.com";
            public const string SupportMobile = "+91 7092661353";
        }
        public static class SP
        {
            public const string ErrorLog = "ErrorLog_I";
            public const string GetUserDetails = "GetUserDetails";
            public const string LoginUpdation = "LoginUpdation";
            public const string UpdatePassword = "UpdatePassword";
            public const string Customer_CRUD = "Customer_CRUD";
            public const string PayIn_CRUD = "PayIn_CRUD";
            public const string PayOut_CRUD = "PayOut_CRUD";
            public const string GetPayInDetailByCustomerId = "GetPayInDetailByCustomerId";
            public const string GetPayOutDetailByCustomerId = "GetPayOutDetailByCustomerId";
            public const string Company_CRUD = "Company_CRUD";
            public const string GetPaymentReport = "PaymentReport";
        }

        public static class API
        {
            public static class AccountService
            {
                public const string GetCategory = "api/CategoryGet";
                public const string ErrorLog = "api/ErrorLog";
            }
            public static class AdminService
            {

            }
        }
    }
}
