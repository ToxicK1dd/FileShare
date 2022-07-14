using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/")]
    [Produces("application/json")]
    [Authorize(Policy = "User")]
    public class BaseController : ControllerBase { }
}