using api.Dtos.PrivateDestination;
using api.Dtos.User;
using api.Exceptions;
using api.Models;
using api.Repositories;
using api.Services;
using api.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;
using Guid = System.Guid;

namespace tests
{
    public class PrivateDestinationServiceTests

    {

        private readonly Mock<DatabaseContext> context;
        private readonly Mock<IPermission> permission;
        private readonly Mock<IConfiguration> configuration;
        
        private readonly PrivateDestinationValidator validator;
        
        private readonly PrivateDestinationsService service;

        public PrivateDestinationServiceTests()
        {
            this.context = new Mock<DatabaseContext>();
            this.permission = new Mock<IPermission>();
            this.configuration = new Mock<IConfiguration>();

            var users = new List<User>().AsQueryable();
            var usersDbSetMock = new Mock<DbSet<User>>();
            usersDbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            usersDbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            usersDbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            usersDbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            this.context.Setup(c => c.Users).Returns(usersDbSetMock.Object);

            var privateDestinations = new List<PrivateDestination>().AsQueryable();
            var privateDestinationsDbSetMock = new Mock<DbSet<PrivateDestination>>();
            privateDestinationsDbSetMock.As<IQueryable<PrivateDestination>>().Setup(m => m.Provider).Returns(privateDestinations.Provider);
            privateDestinationsDbSetMock.As<IQueryable<PrivateDestination>>().Setup(m => m.Expression).Returns(privateDestinations.Expression);
            privateDestinationsDbSetMock.As<IQueryable<PrivateDestination>>().Setup(m => m.ElementType).Returns(privateDestinations.ElementType);
            privateDestinationsDbSetMock.As<IQueryable<PrivateDestination>>().Setup(m => m.GetEnumerator()).Returns(privateDestinations.GetEnumerator());
            this.context.Setup(c => c.PrivateDestinations).Returns(privateDestinationsDbSetMock.Object);
            
            this.validator = new PrivateDestinationValidator();
            this.service = new PrivateDestinationsService(this.context.Object, this.permission.Object, this.validator);
        }

        [Fact]
        public async Task AddAsync_WithValidData_ReturnsPrivateDestinationDto()
        {
            // Arrange
            var newUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Username = "Mario",
                Password = "1234",
                Roles = "Normal"
            };

            this.context.Object.Users.Add(newUser);
            
            var privateDestinationDto = new AddPrivateDestinationDto
            {
                UserId = newUser.Id,
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff",
                StartDate = new DateTime(2002, 9, 30),
                EndDate = new DateTime(2006, 1, 12)
            };

            // Act
            var result = await this.service.AddAsync(privateDestinationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(privateDestinationDto.UserId, result.UserId);
            Assert.Equal(privateDestinationDto.Geolocation, result.Geolocation);
            Assert.Equal(privateDestinationDto.Title, result.Title);
            Assert.Equal(privateDestinationDto.Image, result.Image);
            Assert.Equal(privateDestinationDto.Description, result.Description);
            Assert.Equal(privateDestinationDto.StartDate, result.StartDate);
            Assert.Equal(privateDestinationDto.EndDate, result.EndDate);
        }

        [Fact]
        public async Task AddAsync_WithInvalidGeolocation_ThrowsValidationException()
        {
            // Arrange
            
            var newUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111112"),
                Username = "Mario",
                Password = "1234",
                Roles = "Normal"
            };

            this.context.Object.Users.Add(newUser);
            
            var privateDestinationDto = new AddPrivateDestinationDto
            {
                // Set invalid data that will fail validation
                // For example, set an empty string for a required property
                UserId = newUser.Id,
                Geolocation = "Invalid",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff",
                StartDate = new DateTime(2002, 9, 30),
                EndDate = new DateTime(2006, 1, 12)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(privateDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithEmptyGeolocation_ThrowsValidationException()
        {
            // Arrange
            
            var newUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111112"),
                Username = "Mario",
                Password = "1234",
                Roles = "Normal"
            };

            this.context.Object.Users.Add(newUser);
            
            var privateDestinationDto = new AddPrivateDestinationDto
            {
                // Set invalid data that will fail validation
                // For example, set an empty string for a required property
                UserId = newUser.Id,
                Geolocation = "",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff",
                StartDate = new DateTime(2002, 9, 30),
                EndDate = new DateTime(2006, 1, 12)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(privateDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithInvalidTitle_ThrowsValidationException()
        {
            // Arrange
            
            var newUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111112"),
                Username = "Mario",
                Password = "1234",
                Roles = "Normal"
            };

            this.context.Object.Users.Add(newUser);
            
            var privateDestinationDto = new AddPrivateDestinationDto
            {
                // Set invalid data that will fail validation
                // For example, set an empty string for a required property
                UserId = newUser.Id,
                Geolocation = "latitude: 20, longitude: 80",
                Title = "invalid title",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff",
                StartDate = new DateTime(2002, 9, 30),
                EndDate = new DateTime(2006, 1, 12)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(privateDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithEmptyTitle_ThrowsValidationException()
        {
            // Arrange
            
            var newUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111112"),
                Username = "Mario",
                Password = "1234",
                Roles = "Normal"
            };

            this.context.Object.Users.Add(newUser);
            
            var privateDestinationDto = new AddPrivateDestinationDto
            {
                // Set invalid data that will fail validation
                // For example, set an empty string for a required property
                UserId = newUser.Id,
                Geolocation = "latitude: 20, longitude: 80",
                Title = "",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Good Stuff",
                StartDate = new DateTime(2002, 9, 30),
                EndDate = new DateTime(2006, 1, 12)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(privateDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithInvalidImage_ThrowsValidationException()
        {
            // Arrange
            
            var newUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111112"),
                Username = "Mario",
                Password = "1234",
                Roles = "Normal"
            };

            this.context.Object.Users.Add(newUser);
            
            var privateDestinationDto = new AddPrivateDestinationDto
            {
                // Set invalid data that will fail validation
                // For example, set an empty string for a required property
                UserId = newUser.Id,
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "Not valid image",
                Description = "Good Stuff",
                StartDate = new DateTime(2002, 9, 30),
                EndDate = new DateTime(2006, 1, 12)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(privateDestinationDto));
        }
        
        [Fact]
        public async Task 
            AddAsync_WithEmptyImage_ThrowsValidationException()
        {
            // Arrange
            
            var newUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111112"),
                Username = "Mario",
                Password = "1234",
                Roles = "Normal"
            };

            this.context.Object.Users.Add(newUser);
            
            var privateDestinationDto = new AddPrivateDestinationDto
            {
                // Set invalid data that will fail validation
                // For example, set an empty string for a required property
                UserId = newUser.Id,
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "",
                Description = "Good Stuff",
                StartDate = new DateTime(2002, 9, 30),
                EndDate = new DateTime(2006, 1, 12)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(privateDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithInvalidDescription_ThrowsValidationException()
        {
            // Arrange
            
            var newUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111112"),
                Username = "Mario",
                Password = "1234",
                Roles = "Normal"
            };

            this.context.Object.Users.Add(newUser);
            
            var privateDestinationDto = new AddPrivateDestinationDto
            {
                // Set invalid data that will fail validation
                // For example, set an empty string for a required property
                UserId = newUser.Id,
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and " +
                              "scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of " +
                              "Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                StartDate = new DateTime(2002, 9, 30),
                EndDate = new DateTime(2006, 1, 12)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(privateDestinationDto));
        }
        
        [Fact]
        public async Task AddAsync_WithEmptyDescription_ThrowsValidationException()
        {
            // Arrange
            
            var newUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111112"),
                Username = "Mario",
                Password = "1234",
                Roles = "Normal"
            };

            this.context.Object.Users.Add(newUser);
            
            var privateDestinationDto = new AddPrivateDestinationDto
            {
                // Set invalid data that will fail validation
                // For example, set an empty string for a required property
                UserId = newUser.Id,
                Geolocation = "latitude: 20, longitude: 80",
                Title = "FSEGA",
                Image = "https://fastly.picsum.photos/id/352/200/300.jpg?hmac=JRE6d4eB1tvPUpBESG8XEM2_22EaXNe2luRrVkydr2E",
                Description = "",
                StartDate = new DateTime(2002, 9, 30),
                EndDate = new DateTime(2006, 1, 12)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => this.service.AddAsync(privateDestinationDto));
        }
    }
}