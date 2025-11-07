using BE;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace Factura.Controllers
{
    public class AdminController : BaseController
    {
        private new readonly IConfiguration _Configure;
        private new readonly APIURL URL;
        private readonly ApiClient apiclient;
        public AdminController(IConfiguration configuration) : base(configuration)
        {
            _Configure = configuration;
            URL = GetApiLinks();
            apiclient = new ApiClient(URL.AdminServiceURL, "1");
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
