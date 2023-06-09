using System.ComponentModel.DataAnnotations;
using api.Dtos.PublicDestinations;
using api.Models;
using api.Repositories;
using api.Services;
using api.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using api.Exceptions;
using ValidationException = api.Exceptions.ValidationException;

namespace tests;

public class PublicDestinationServiceTests
{
    private readonly Mock<DatabaseContext> context;
        private readonly Mock<IPermission> permission;
        private readonly Mock<IConfiguration> configuration;
        
        private readonly PublicDestinationValidator validator;
        
        private readonly PublicDestinationsService service;

        public PublicDestinationServiceTests()
        {
            this.context = new Mock<DatabaseContext>();
            this.permission = new Mock<IPermission>();
            this.configuration = new Mock<IConfiguration>();

            var publicDestinations = new List<Destination>().AsQueryable();
            var publicDestinationsDbSetMock = new Mock<DbSet<Destination>>();
            publicDestinationsDbSetMock.As<IQueryable<Destination>>().Setup(m => m.Provider).Returns(publicDestinations.Provider);
            publicDestinationsDbSetMock.As<IQueryable<Destination>>().Setup(m => m.Expression).Returns(publicDestinations.Expression);
            publicDestinationsDbSetMock.As<IQueryable<Destination>>().Setup(m => m.ElementType).Returns(publicDestinations.ElementType);
            publicDestinationsDbSetMock.As<IQueryable<Destination>>().Setup(m => m.GetEnumerator()).Returns(publicDestinations.GetEnumerator());
            this.context.Setup(c => c.PublicDestinations).Returns(publicDestinationsDbSetMock.Object);
            
            this.validator = new PublicDestinationValidator();
            this.service = new PublicDestinationsService(this.context.Object, this.validator);
        }
        
        [Fact]
        public async Task AddAsync_WithValidData_ReturnsPrivateDestinationDto()
        {
            // Arrange
            var  publicDestinationDto = new AddPublicDestinationDto
            {
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff"
            };

            // Act
            var result = await this.service.AddAsync(publicDestinationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publicDestinationDto.Geolocation, result.Geolocation);
            Assert.Equal(publicDestinationDto.Title, result.Title);
            Assert.Equal(publicDestinationDto.Image, result.Image);
            Assert.Equal(publicDestinationDto.Description, result.Description);
        }

        [Fact]
        public async Task AddAsync_WithInvalidGeolocation_ThrowsValidationException()
        {
            // Arrange
            var publicDestinationDto = new AddPublicDestinationDto
            {
                Geolocation = "Invalid Geolocation",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff"
            };

            // Act & Assert
            await Assert.ThrowsAsync<api.Exceptions.ValidationException>(() => this.service.AddAsync(publicDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithEmptyGeolocation_ThrowsValidationException()
        {
            // Arrange
            var publicDestinationDto = new AddPublicDestinationDto
            {
                Geolocation = "",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(publicDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithInvalidTitle_ThrowsValidationException()
        {
            // Arrange
            var  publicDestinationDto = new AddPublicDestinationDto
            {
                Geolocation = "latitude: 20, longitude: 80",
                Title = "invalid title",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(publicDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithEmptyTitle_ThrowsValidationException()
        {
            // Arrange
            var  publicDestinationDto = new AddPublicDestinationDto
            {
                Geolocation = "latitude: 20, longitude: 80",
                Title = "",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(publicDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithInvalidImage_ThrowsValidationException()
        {
            // Arrange
            var  publicDestinationDto = new AddPublicDestinationDto
            {
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "Not a valid image",
                Description = "Good Stuff"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(publicDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithEmptyImage_ThrowsValidationException()
        {
            // Arrange
            var  publicDestinationDto = new AddPublicDestinationDto
            {
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "",
                Description = "Good Stuff"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(publicDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithInvalidDescription_ThrowsValidationException()
        {
            // Arrange
            var publicDestinationDto = new AddPublicDestinationDto
            {
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and " +
                              "scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of " +
                              "Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(publicDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithEmptyDescription_ThrowsValidationException()
        {
            // Arrange
            var publicDestinationDto = new AddPublicDestinationDto
            {
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = ""
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(publicDestinationDto));
        }
}