using FileModel = FileShare.DataAccess.Models.Primary.Document.Document;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Dtos.V2._0.Document;
using FileShare.Service.Services.V2._0.Document.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using SharpCompress.Compressors.Deflate;
using SharpCompress.Compressors;
using SharpCompress.Readers;

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
            var userId = await GetUserId(cancellationToken);
            var fileModel = new FileModel()
            {
                UserId = userId,
                Detail = new()
                {
                    FileName = file.FileName,
                    Extention = Path.GetExtension(file.FileName),
                    ContentType = file.ContentType,
                    Length = file.Length
                }
            };

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);

            var compressedFile = await CompressFile(memoryStream.ToArray(), cancellationToken);
            fileModel.Contents = compressedFile;

            await _unitOfWork.DocumentRepository.AddAsync(fileModel, cancellationToken);
            return fileModel.Id;
        }

        public async Task<FileDto> DownloadFileAsync(Guid id, CancellationToken cancellationToken)
        {
            var userId = await GetUserId(cancellationToken);
            var dbFile = await GetDocumentAsync(id, userId, cancellationToken);

            return new FileDto()
            {
                FileContents = await DecompressFile(dbFile.Contents, cancellationToken),
                FileName = dbFile.Detail.FileName,
                ContentType = dbFile.Detail.ContentType
            };
        }

        public async Task DeleteFileAsync(Guid id, CancellationToken cancellationToken)
        {
            var userId = await GetUserId(cancellationToken);
            var dbFile = await GetDocumentAsync(id, userId, cancellationToken);

            _unitOfWork.DocumentRepository.RemoveById(dbFile.Id);
        }


        #region Helpers

        private async Task<FileModel> GetDocumentAsync(Guid id, Guid userId, CancellationToken cancellationToken)
        {
            var dbFile = await _unitOfWork.DocumentRepository.GetByIdWithDetailAsync(id, cancellationToken);
            if (dbFile is null)
                throw new KeyNotFoundException($"File with id {id} was not found.");
            if (dbFile.UserId != userId)
                throw new UnauthorizedAccessException($"File with id {id} does not belong to this user.");

            return dbFile;
        }

        private async Task<Guid> GetUserId(CancellationToken cancellationToken)
        {
            var username = _identityClaimsHelper.GetUsernameFromHttpContext(_httpContextAccessor.HttpContext);
            if (username is null)
                throw new UnauthorizedAccessException();

            return await _unitOfWork.UserRepository.GetIdByUsernameAsync(username, cancellationToken);
        }

        private async Task<byte[]> CompressFile(byte[] buffer, CancellationToken cancellationToken)
        {
            using MemoryStream stream = new();
            using (GZipStream zip = new(stream, CompressionMode.Compress))
            {
                await zip.WriteAsync(buffer.AsMemory(0, buffer.Length), cancellationToken);
                zip.Close();
            }
            return stream.ToArray();
        }

        private async Task<byte[]> DecompressFile(byte[] buffer, CancellationToken cancellationToken)
        {
            using MemoryStream stream = new(buffer);
            using IReader reader = ReaderFactory.Open(stream);
            while (reader.MoveToNextEntry())
            {
                if (!reader.Entry.IsDirectory)
                {
                    using MemoryStream ms = new();

                    using var entryStream = reader.OpenEntryStream();
                    await entryStream.CopyToAsync(ms, cancellationToken);

                    return ms.ToArray();
                }
            }

            return null;
        }

        #endregion
    }
}