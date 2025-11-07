using BE;
using DL.Implementation;
using DL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _accountRepository { get; set; }

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("api/ErrorLog")]
        public IActionResult ErrorLog([FromBody] ErrorLog errorLog)
        {
            _accountRepository.ErrorLog(errorLog);
            return Ok();
        }
    }
}
