using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Document.Interface;
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
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        [ActionName("File")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var accountId = HttpContext.GetAccountIdFromHttpContext();
            var image = new FileModel()
            {
                AccountId = accountId,
                Detail = new()
                {
                    FileName = file.FileName,
                    Extention = Path.GetExtension(file.FileName),
                    ContentType = file.ContentType,
                    Length = file.Length
                }
            };

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            image.Blob = fileBytes;

            await _unitOfWork.DocumentRepository.AddAsync(image, HttpContext.RequestAborted);
            await _unitOfWork.SaveChangesAsync(HttpContext.RequestAborted);

            return Created(string.Empty, new
            {
                image.Id
            });
        }

        /// <summary>
        /// Download a file.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("{id}")]
        [ActionName("File")]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var accountId = HttpContext.GetAccountIdFromHttpContext();
            if (accountId == Guid.Empty)
                return Unauthorized();

            var dbFile = await _unitOfWork.DocumentRepository.GetByIdWithDetailAsync(id, HttpContext.RequestAborted);
            if (dbFile is null)
                return NotFound();
            if (dbFile.AccountId != accountId)
                throw new Exception("This file does not belong to this user.");

            var cd = new ContentDisposition
            {
                FileName = dbFile.Detail.FileName,
                Inline = false,
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());

            return File(dbFile.Blob, dbFile.Detail.ContentType, dbFile.Detail.FileName);
        }

        [HttpDelete]
        [Route("{id}")]
        [ActionName("File")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            var accountId = HttpContext.GetAccountIdFromHttpContext();
            if (accountId == Guid.Empty)
                return Unauthorized();

            var dbFile = await _unitOfWork.DocumentRepository.GetByIdAsync(id, HttpContext.RequestAborted);
            if (dbFile is null)
                return NotFound();
            if (dbFile.AccountId != accountId)
                throw new Exception("This file does not belong to this user.");

            _unitOfWork.DocumentRepository.RemoveById(id);
            await _unitOfWork.SaveChangesAsync(HttpContext.RequestAborted);

            return NoContent();
        }
    }
}