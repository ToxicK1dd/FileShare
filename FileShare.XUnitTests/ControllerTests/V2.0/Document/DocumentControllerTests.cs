using FileShare.Api.Controllers.V2._0.Document;
using FileShare.Api.Models.V2._0.Document;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.Document.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ControllerTests.V2._0.Document
{
    public class DocumentControllerTests
    {
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IDocumentService> _mockDocumentService;
        private readonly DocumentController _documentController;

        public DocumentControllerTests()
        {
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockDocumentService = new Mock<IDocumentService>();
            _documentController = new DocumentController(_mockUnitOfWork.Object, _mockDocumentService.Object);
        }


        [Fact]
        public async Task UploadFile_ShouldReturnCreated_WhenFileIsUploaded()
        {
            // Arrange
            var fileId = Guid.NewGuid();

            using var stream = new MemoryStream();
            IFormFile file = new FormFile(stream, 0, stream.Length, "test", "test.pdf");

            _mockDocumentService.Setup(service => service.UploadFileAsync(It.IsAny<IFormFile>()))
                .ReturnsAsync(fileId);

            // Act
            var result = await _documentController.UploadFile(file);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var dto = Assert.IsType<UploadFileModel>(createdResult.Value);
            Assert.Equal(fileId, dto.FileId);

            _mockDocumentService.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
        }
    }
}