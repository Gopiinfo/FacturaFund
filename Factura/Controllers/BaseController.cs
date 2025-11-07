using BE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utility;

namespace Factura.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IConfiguration _Configure;
        protected APIURL URL;

        public int UserID = 0;
        public DateTime CreatedDate = DateTime.Now;

        protected BaseController(IConfiguration configuration)
        {
            _Configure = configuration;
            BaseFilePath = Path.Combine(_Configure.GetValue<string>("S3:folder"));
            BucketName = Path.Combine(_Configure.GetValue<string>("S3:bucket"));
        }

        protected APIURL GetApiLinks()
        {
            URL = new APIURL()
            {
                AccountServiceURL = _Configure.GetValue<string>("ServiceURL:AccountServiceURL"),
                AdminServiceURL = _Configure.GetValue<string>("ServiceURL:AdminServiceURL"),
                CustomerServiceURL = _Configure.GetValue<string>("ServiceURL:CustomerServiceURL")
            };
            return URL;
        }
        public string BaseFilePath { get; set; }
        public string BucketName { get; set; }
    }
}
