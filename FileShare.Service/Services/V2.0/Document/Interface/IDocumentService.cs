using FileShare.Service.Dtos.V2._0.Document;
using Microsoft.AspNetCore.Http;

namespace FileShare.Service.Services.V2._0.Document.Interface
{
    public interface IDocumentService
    {
        Task<Guid> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);
        Task<FileDto> DownloadFileAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteFileAsync(Guid id, CancellationToken cancellationToken);
    }
}