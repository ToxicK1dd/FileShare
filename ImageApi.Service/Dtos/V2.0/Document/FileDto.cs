namespace ImageApi.Service.Dtos.V2._0.Document
{
    public class FileDto
    {
        public byte[] FileContents { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }
    }
}