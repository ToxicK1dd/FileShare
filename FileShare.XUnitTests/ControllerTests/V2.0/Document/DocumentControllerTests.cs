using FileShare.Api.Dtos.V2._0.Document;
using FileShare.Api.Controllers.V2._0.Document;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Document.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ControllerTests.V2._0.Document
{
    public class DocumentControllerTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<DocumentController>> _mockLogger;
        private readonly Mock<IDocumentService> _mockDocumentService;
        private readonly DocumentController _documentController;

        public DocumentControllerTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockLogger = new Mock<ILogger<DocumentController>>();
            _mockDocumentService = new Mock<IDocumentService>();
            _documentController = new DocumentController(_mockHttpContextAccessor.Object, _mockUnitOfWork.Object, _mockLogger.Object, _mockDocumentService.Object);
        }


        [Fact]
        public async Task UploadFile_ShouldReturnCreated_WhenFileIsUploaded()
        {
            // Arrange
            var fileId = Guid.NewGuid();

            using var stream = new MemoryStream();
            IFormFile file = new FormFile(stream, 0, stream.Length, "test", "test.pdf");

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockDocumentService.Setup(service => service.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fileId);

            // Act
            var result = await _documentController.UploadFile(file);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var dto = Assert.IsType<UploadFileDto>(createdResult.Value);
            Assert.Equal(fileId, dto.FileId);

            _mockDocumentService.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}