using ImageApi.Service.Services.Document.Interface;

namespace ImageApi.Controllers.Document
{
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