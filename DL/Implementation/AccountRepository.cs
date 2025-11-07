using BE;
using DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DL.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        public void ErrorLog(ErrorLog log)
        {
            try
            {
                DataHelper.LogErrorinDB(log.LoginId, log.Url, log.ErrorMessage, log.StackTrace);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
