using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Api.Models.V2._0.Registration;
using FileShare.Service.Services.Registration.Interface;
using FileShare.Service.Services.Token.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using FileShare.Service.Dtos.Registration;

namespace FileShare.Api.Controllers.V2._0.Registration
{
    /// <summary>
    /// Endpoints for registering users.
    /// </summary>
    [ApiVersion("2.0")]
    public class RegistrationController : BaseController
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IRegistrationService _registrationService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public RegistrationController(
            IPrimaryUnitOfWork unitOfWork,
            IRegistrationService registrationService,
            ITokenService tokenService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _registrationService = registrationService;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        /// <summary>
        /// Registers a new account.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///        "username": "Superman",
        ///        "email": "superman@kryptonmail.space",
        ///        "password": "!Krypton1t3",
        ///        "confirmPassword": "!Krypton1t3"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns a newly created access token, and refresh token.</response>
        /// <response code="500">The username, or email is already in use.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            var dto = _mapper.Map<RegisterDto>(model);

            var result = await _registrationService.RegisterAsync(dto);
            if (result.Successful is false)
                return Problem(result.ErrorMessage, statusCode: 400);

            var token = await _tokenService.GetAccessTokenFromUsernameAsync(dto.Username);
            var refreshToken = await _tokenService.GetRefreshTokenFromUsernameAsync(dto.Username);

            await _unitOfWork.SaveChangesAsync();

            return Created(string.Empty, new
            {
                token,
                refreshToken
            });
        }
    }
}