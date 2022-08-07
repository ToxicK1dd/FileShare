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
        /// <returns>The id of the saved file.</returns>
        Task<Guid> UploadFileAsync(IFormFile file);

        /// <summary>
        /// Gets a file from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The file if found, otherwise null.</returns>
        Task<FileDto> DownloadFileAsync(Guid id);

        /// <summary>
        /// Deletes a file from the database.
        /// </summary>
        /// <param name="id"></param>
        Task DeleteFileAsync(Guid id);
    }
}