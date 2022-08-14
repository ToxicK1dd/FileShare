using FileModel = FileShare.DataAccess.Models.Primary.Document.Document;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Dtos.Document;
using FileShare.Service.Services.Document.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using SharpCompress.Compressors.Deflate;
using SharpCompress.Compressors;
using SharpCompress.Readers;

namespace FileShare.Service.Services.Document
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


        public async Task<Guid> UploadFileAsync(IFormFile file)
        {
            var userId = await GetUserId();
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
            await file.CopyToAsync(memoryStream, _httpContextAccessor.HttpContext.RequestAborted);

            var compressedFile = await CompressFile(memoryStream.ToArray());
            fileModel.Contents = compressedFile;

            await _unitOfWork.DocumentRepository.AddAsync(fileModel);
            return fileModel.Id;
        }

        public async Task<FileDto> DownloadFileAsync(Guid id)
        {
            var userId = await GetUserId();
            var dbFile = await GetDocumentAsync(id, userId);

            return new FileDto()
            {
                FileContents = await DecompressFile(dbFile.Contents),
                FileName = dbFile.Detail.FileName,
                ContentType = dbFile.Detail.ContentType
            };
        }

        public async Task DeleteFileAsync(Guid id)
        {
            var userId = await GetUserId();
            var dbFile = await GetDocumentAsync(id, userId);

            _unitOfWork.DocumentRepository.RemoveById(dbFile.Id);
        }


        #region Helpers

        private async Task<FileModel> GetDocumentAsync(Guid id, Guid userId)
        {
            var dbFile = await _unitOfWork.DocumentRepository.GetByIdWithDetailAsync(id, _httpContextAccessor.HttpContext.RequestAborted);
            if (dbFile is null)
                throw new KeyNotFoundException($"File with id {id} was not found.");
            if (dbFile.UserId != userId)
                throw new UnauthorizedAccessException($"File with id {id} does not belong to this user.");

            return dbFile;
        }

        private async Task<Guid> GetUserId()
        {
            var username = _identityClaimsHelper.GetUsernameFromHttpContext(_httpContextAccessor.HttpContext);
            if (username is null)
                throw new UnauthorizedAccessException();

            return await _unitOfWork.UserRepository.GetIdByUsernameAsync(username, _httpContextAccessor.HttpContext.RequestAborted);
        }

        private async Task<byte[]> CompressFile(byte[] buffer)
        {
            using MemoryStream stream = new();
            using (GZipStream zip = new(stream, CompressionMode.Compress))
            {
                await zip.WriteAsync(buffer.AsMemory(0, buffer.Length), _httpContextAccessor.HttpContext.RequestAborted);
                zip.Close();
            }
            return stream.ToArray();
        }

        private async Task<byte[]> DecompressFile(byte[] buffer)
        {
            using MemoryStream stream = new(buffer);
            using IReader reader = ReaderFactory.Open(stream);
            while (reader.MoveToNextEntry())
            {
                if (!reader.Entry.IsDirectory)
                {
                    using MemoryStream ms = new();

                    using var entryStream = reader.OpenEntryStream();
                    await entryStream.CopyToAsync(ms, _httpContextAccessor.HttpContext.RequestAborted);

                    return ms.ToArray();
                }
            }

            return null;
        }

        #endregion
    }
}