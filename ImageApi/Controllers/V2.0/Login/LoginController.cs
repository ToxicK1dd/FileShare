﻿using ImageApi.Service.Services.Login.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers.V2._0.Login
{
    [ApiVersion("2.0")]
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;

        public LoginController(ILogger<LoginController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }
    }
}