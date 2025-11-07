using BE;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Utility;

namespace Factura.Controllers
{
    public class AccountController : BaseController
    {
        private new readonly IConfiguration _Configure;
        private new readonly APIURL URL;
        private readonly ApiClient apiclient;
        public AccountController(IConfiguration configuration) : base(configuration)
        {
            _Configure = configuration;
            URL = GetApiLinks();
            apiclient = new ApiClient(URL.AccountServiceURL, "1");
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            HttpResponseMessage? Response = null;

            ErrorLog log = new ErrorLog()
            {
                LoginId = "0",
                Url = exceptionDetails?.Path,
                ErrorMessage = exceptionDetails?.Error?.Message,
                StackTrace = exceptionDetails?.Error?.StackTrace
            };

            Response = await apiclient.Post(Constant.API.AccountService.ErrorLog, log);
            if (Response.IsSuccessStatusCode)
            {
                await Response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("ErrorLog");
        }

        public async Task<IActionResult> ErrorLog()
        {
            return View();
        }
    }
}
