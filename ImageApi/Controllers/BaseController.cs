using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers
{
    [ApiController]
    [Authorize(Policy = "User")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BaseController : ControllerBase { }
}