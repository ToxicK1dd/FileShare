using FileShare.Api.Models.V2._0.Document;
using Swashbuckle.AspNetCore.Filters;

namespace FileShare.Api.Examples.V2._0.Document
{
    public class UploadFileModelExample : IExamplesProvider<UploadFileModel>
    {
        public UploadFileModel GetExamples()
        {
            return new() { FileId = Guid.NewGuid() };
        }
    }
}