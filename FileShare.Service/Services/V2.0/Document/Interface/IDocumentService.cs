using FileShare.Service.Dtos.V2._0.Document;
using Microsoft.AspNetCore.Http;

namespace FileShare.Service.Services.V2._0.Document.Interface
{
    public interface IDocumentService
    {
        /// <summary>
        /// Saves a file to the database.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The id of the saved file.</returns>
        Task<Guid> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a file from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The file if found, otherwise null.</returns>
        Task<FileDto> DownloadFileAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a file from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        Task DeleteFileAsync(Guid id, CancellationToken cancellationToken);
    }
}