using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Dtos.V2._0.Document;
using FileShare.Service.Services.V2._0.Document.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using FileModel = FileShare.DataAccess.Models.Primary.Document.Document;

namespace FileShare.Service.Services.V2._0.Document
{
    /// <summary>
    /// Methods for saving, and retrieving documents.
    /// </summary>
    public class DocumentService : IDocumentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IIdentityClaimsHelper _identityClaimsHelper;

        public DocumentService(
            IHttpContextAccessor httpContextAccessor,
            IPrimaryUnitOfWork unitOfWork,
            IIdentityClaimsHelper identityClaimsHelper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _identityClaimsHelper = identityClaimsHelper;
        }


        public async Task<Guid> UploadFileAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            var fileModel = new FileModel()
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
            await file.CopyToAsync(ms, cancellationToken);
            var fileBytes = ms.ToArray();
            fileModel.Contents = fileBytes;

            await _unitOfWork.DocumentRepository.AddAsync(fileModel, cancellationToken);
            return fileModel.Id;
        }

        public async Task<FileDto> DownloadFileAsync(Guid id, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            var dbFile = await GetDocumentAsync(id, accountId, cancellationToken);

            return new FileDto()
            {
                FileContents = dbFile.Contents,
                FileName = dbFile.Detail.FileName,
                ContentType = dbFile.Detail.ContentType
            };
        }

        public async Task DeleteFileAsync(Guid id, CancellationToken cancellationToken)
        {
            var accountId = GetAccountId();
            var dbFile = await GetDocumentAsync(id, accountId, cancellationToken);

            _unitOfWork.DocumentRepository.RemoveById(dbFile.Id);
        }


        #region Helpers

        public async Task<FileModel> GetDocumentAsync(Guid id, Guid accountId, CancellationToken cancellationToken)
        {
            var dbFile = await _unitOfWork.DocumentRepository.GetByIdWithDetailAsync(id, cancellationToken);
            if (dbFile is null)
                throw new KeyNotFoundException($"File with id {id} was not found.");
            if (dbFile.AccountId != accountId)
                throw new UnauthorizedAccessException($"File with id {id} does not belong to this user.");

            return dbFile;
        }

        public Guid GetAccountId()
        {
            var accountId = _identityClaimsHelper.GetAccountIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (accountId == Guid.Empty)
                throw new UnauthorizedAccessException();

            return accountId;
        }

        #endregion
    }
}