using ImageApi.Service.Services.Account.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers.V1._0.Account
{
    [ApiVersion("1.0")]
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }
    }
}