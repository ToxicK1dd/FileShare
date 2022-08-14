using FileShare.Service.Services.Share.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Controllers.V2._0.Share
{
    [ApiVersion("2.0")]
    public class ShareController : BaseController
    {
        private readonly IShareService _shareService;

        public ShareController(IShareService shareService)
        {
            _shareService = shareService;
        }
    }
}