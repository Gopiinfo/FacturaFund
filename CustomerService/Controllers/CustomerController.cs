using BE;
using BL.Customer;
using DL.Implementation;
using DL.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace CustomerService.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository { get; set; }

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        [HttpGet]
        [Route("api/GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            List<Customer> res = await _customerRepository.GetAllCustomer();
            return Ok(res);
        }


        [HttpPost]
        [Route("api/CustomerInsert")]
        public async Task<IActionResult> CustomerInsert([FromBody] CustomerWizardInput input)
        {
            DatatableInput datatable = CustomerBL.ConvertToDataTable(input);
            long res = await _customerRepository.CustomerInsert(datatable);
            if (res == -1)
                return BadRequest();
            else
                return Ok(res);
        }

        [HttpPost]
        [Route("api/ErrorLog")]
        public IActionResult ErrorLog([FromBody] ErrorLog errorLog)
        {
            _customerRepository.ErrorLog(errorLog);
            return Ok();
        }
    }
}
