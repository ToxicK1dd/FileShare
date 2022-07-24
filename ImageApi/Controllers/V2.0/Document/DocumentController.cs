using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.V2._0.Document.Interface;
using ImageApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using FileModel = ImageApi.DataAccess.Models.Primary.Document.Document;

namespace ImageApi.Controllers.V2._0.Document
{
    /// <summary>
    /// Endpoints for managing documents
    /// </summary>
    [ApiVersion("2.0")]
    public class DocumentController : BaseController
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly ILogger<DocumentController> _logger;
        private readonly IDocumentService _documentService;

        public DocumentController(
            IPrimaryUnitOfWork unitOfWork,
            ILogger<DocumentController> logger,
            IDocumentService documentService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _documentService = documentService;
        }


        /// <summary>
        ///  Upload a file.
        /// </summary>
        /// <param name="file"></param>
        /// <response code="201">Returns the uuid of the uploaded document.</response>
        [HttpPost]
        [DisableRequestSizeLimit]
        [ActionName("File")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var fileId = await _documentService.UploadFileAsync(file, HttpContext.RequestAborted);
            await _unitOfWork.SaveChangesAsync(HttpContext.RequestAborted);

            return Created(string.Empty, new
            {
                fileId
            });
        }

        /// <summary>
        /// Download a file.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns the document.</response>
        /// <response code="404">The specified document could not be found</response>
        [HttpGet]
        [Route("{id}")]
        [ActionName("File")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var file = await _documentService.DownloadFileAsync(id, HttpContext.RequestAborted);

            var cd = new ContentDisposition
            {
                FileName = file.FileName,
                Inline = false,
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());

            return Ok(File(file.FileContents, file.ContentType, file.FileName));
        }

        /// <summary>
        /// Delete a file.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">The document was successfully deleted.</response>
        /// <response code="404">The specified document could not be found.</response>
        [HttpDelete]
        [Route("{id}")]
        [ActionName("File")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            await _documentService.DeleteFileAsync(id, HttpContext.RequestAborted);
            await _unitOfWork.SaveChangesAsync(HttpContext.RequestAborted);

            return NoContent();
        }
    }
}