using BE;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using Utility;
using VM;
using SE;
namespace Factura.Controllers
{
    public class CustomerController : BaseController
    {
        private new readonly IConfiguration _Configure;
        private new readonly APIURL URL;
        private readonly ApiClient apiclient;
        private readonly Common Common;
        HttpResponseMessage? Response = null;
        public CustomerController(IConfiguration configuration) : base(configuration)
        {
            _Configure = configuration;
            URL = GetApiLinks();
            apiclient = new ApiClient(URL.CustomerServiceURL, "1");
            Common = new Common(_Configure);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Customer> customerList = new List<Customer>();
            Response = await apiclient.Get(Constant.API.CustomerService.GetAllCustomer);
            if (Response.IsSuccessStatusCode)
            {
                var customerListJsonString = await Response.Content.ReadAsStringAsync();
                customerList = JsonConvert.DeserializeObject<List<Customer>>(customerListJsonString);
            }
            return View(customerList);
        }

        [HttpGet]
        public IActionResult CustomerDetails()
        {
            CustomerDetailsVM vm = GetCustomerDetails();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CustomerDetails(CustomerDetailsVM vm, string btnNext)
        {
            if (btnNext != null)
            {
                if (ModelState.IsValid)
                {
                    SetCustomerDetails(vm);
                    return RedirectToAction("LoanDetails");
                }
            }

            return View();
        }


        [HttpGet]
        public IActionResult LoanDetails()
        {
            LoanDetailsVM vm = GetLoanDetails();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoanDetails(LoanDetailsVM vm, string btnPrev, string btnNext)
        {
            if (btnNext != null)
            {
                ModelState.Remove("btnPrev");
                if (ModelState.IsValid)
                {
                    SetLoanDetails(vm);
                    return RedirectToAction("BankDetails");
                }
            }
            if (btnPrev != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult BankDetails()
        {
            BankDetailsVM vm = GetBankDetails();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BankDetails(BankDetailsVM vm, string btnPrev, string btnNext)
        {
            if (btnNext != null)
            {
                ModelState.Remove("btnPrev");
                if (ModelState.IsValid)
                {
                    SetBankDetails(vm);
                    return RedirectToAction("DocumentDetails");
                }
            }
            if (btnPrev != null)
            {
                return RedirectToAction("LoanDetails");
            }
            return View();
        }

        [HttpGet]
        public IActionResult DocumentDetails()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DocumentDetails(DocumentDetailsVM vm, string btnPrev, string btnFinish)
        {
            if (btnFinish != null)
            {
                ModelState.Remove("btnPrev");
                if (ModelState.IsValid)
                {
                    CustomerWizardInput input = new CustomerWizardInput
                    {
                        CustomerDetails = GetCustomerDetails(),
                        LoanDetails = GetLoanDetails(),
                        BankDetails = GetBankDetails(),
                        DocumentDetails = GetDocumentFileNames(vm)
                    };

                    Response = await apiclient.Post(Constant.API.CustomerService.CustomerInsert, input);
                    var customerJsonString = await Response.Content.ReadAsStringAsync();
                    int CustomerId = JsonConvert.DeserializeObject<int>(customerJsonString);

                    if (Response.IsSuccessStatusCode)
                    {
                        string savePath = $"{BaseFilePath}/{CustomerId.ToString()}";
                        if (await Common.UploadFileS3Async(vm, BucketName, savePath))
                        {
                            HttpContext.Session.Clear();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            //Error Message -- Upload Failed.Contact Administrator!
                        }
                    }
                    else
                    {
                        //Error Message -- Insertion Failed.Contact Administrator!
                    }
                }
            }
            if (btnPrev != null)
            {
                return RedirectToAction("BankDetails");
            }
            return View();
        }

        private CustomerDetailsVM GetCustomerDetails()
        {
            CustomerDetailsVM vm = new CustomerDetailsVM();
            var customerDetailsJson = HttpContext.Session.GetString("CustomerDetails");
            if (customerDetailsJson != null)
                vm = JsonConvert.DeserializeObject<CustomerDetailsVM>(customerDetailsJson);
            return vm;
        }

        private void SetCustomerDetails(CustomerDetailsVM vm)
        {
            HttpContext.Session.SetString("CustomerDetails", JsonConvert.SerializeObject(vm));
        }

        private BankDetailsVM GetBankDetails()
        {
            BankDetailsVM vm = new BankDetailsVM();
            var BankDetailsJson = HttpContext.Session.GetString("BankDetails");
            if (BankDetailsJson != null)
                vm = JsonConvert.DeserializeObject<BankDetailsVM>(BankDetailsJson);
            return vm;
        }

        private void SetBankDetails(BankDetailsVM vm)
        {
            HttpContext.Session.SetString("BankDetails", JsonConvert.SerializeObject(vm));
        }

        private LoanDetailsVM GetLoanDetails()
        {
            LoanDetailsVM vm = new LoanDetailsVM();
            var LoanDetailsJson = HttpContext.Session.GetString("LoanDetails");
            if (LoanDetailsJson != null)
                vm = JsonConvert.DeserializeObject<LoanDetailsVM>(LoanDetailsJson);
            return vm;
        }

        private void SetLoanDetails(LoanDetailsVM vm)
        {
            HttpContext.Session.SetString("LoanDetails", JsonConvert.SerializeObject(vm));
        }

        private DocumentDetails GetDocumentFileNames(DocumentDetailsVM vm)
        {
            DocumentDetails _vm = new DocumentDetails();
            _vm.Aadhaar = "Aadhaar.jpg";
            _vm.BankPassBook = "BankPassBook.jpg";
            _vm.Singnature = "Singnature.jpg";

            return _vm;
        }

        public async Task<IActionResult> DownloadFile(string FileName, string CustomerId)
        {
            FileName = Secure.DeCrypt(FileName);
            CustomerId = Secure.DeCrypt(CustomerId);

            string savePath = $"{BaseFilePath}/{CustomerId.ToString()}";
            FileStatus fileResult = await Common.DownloadFileS3Async(BucketName, savePath, FileName);
            return File(fileResult.memoryStream, fileResult.contentType, string.Concat(CustomerId, "_", fileResult.fileName));
        }


        public async Task<IActionResult> ViewFile(string FileName, string CustomerId)
        {
            FileName = Secure.DeCrypt(FileName);
            CustomerId = Secure.DeCrypt(CustomerId);

            string savePath = $"{BaseFilePath}/{CustomerId.ToString()}";
            FileStatus fileResult = await Common.DownloadFileS3Async(BucketName, savePath, FileName);
            fileResult.fileBytes = fileResult.memoryStream.ToArray();

            return File(fileResult.fileBytes, fileResult.contentType);
        }

    }
}
