using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Utility
    {
        private readonly IConfiguration _config;
        public Utility(IConfiguration configuration)
        {
            _config = configuration;
        }

        public static string ConnectionString()
        {
            string c = Directory.GetCurrentDirectory();
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appSettings.json").Build();
            string connectionString = configuration.GetConnectionString("CMS");
            return connectionString;
        }

        public static void LogError(string Login_ID, string errmoduleName, string errprocedureName, string errorObject)
        {
            DataHelper.LogErrorinDB(Login_ID, errmoduleName, errprocedureName, errorObject);
        }

        public static string DateFormat(string Date)
        {
            string res = string.Empty;
            if (!string.IsNullOrEmpty(Date))
            {
                if (Date.IndexOf("/") != -1)
                {
                    string[] dateArr = Date.Split('/');
                    res = dateArr[1] + "/" + dateArr[0] + "/" + dateArr[2];
                }
            }
            else
            {
                res = Date;
            }
            return res;
        }

        public static string DateFormatForDB(string Date)
        {
            string res = string.Empty;
            if (!string.IsNullOrEmpty(Date))
            {
                if (Date.IndexOf("/") != -1)
                {
                    string[] dateArr = Date.Split('/');
                    res = dateArr[2] + "-" + dateArr[1] + "-" + dateArr[0];
                }
            }
            else
            {
                res = Date;
            }
            return res;
        }
    }
}
