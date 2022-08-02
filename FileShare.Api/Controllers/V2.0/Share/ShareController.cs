using FileShare.Service.Services.V2._0.Share.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Controllers.V2._0.Share
{
    [ApiVersion("2.0")]
    public class ShareController : BaseController
    {
        private readonly ILogger<ShareController> _logger;
        private readonly IShareService _shareService;

        public ShareController(ILogger<ShareController> logger, IShareService shareService)
        {
            _logger = logger;
            _shareService = shareService;
        }
    }
}