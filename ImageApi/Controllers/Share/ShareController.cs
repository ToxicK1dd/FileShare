using ImageApi.Service.Services.Share.Interface;

namespace ImageApi.Controllers.Share
{
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