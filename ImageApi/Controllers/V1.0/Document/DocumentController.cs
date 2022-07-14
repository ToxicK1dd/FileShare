using ImageApi.Service.Services.Document.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers.V1._0.Document
{
    [ApiVersion("1.0")]
    public class DocumentController : BaseController
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly IDocumentService _documentService;

        public DocumentController(ILogger<DocumentController> logger, IDocumentService documentService)
        {
            _logger = logger;
            _documentService = documentService;
        }
    }
}