using api.Dtos.User;
using api.Exceptions;
using api.Models;
using api.Repositories;
using api.Services;
using api.Validators;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace tests
{
    public class AuthServiceTests
    {

        private readonly Mock<DatabaseContext> context;
        private readonly Mock<IConfiguration> configuration;
        private readonly AuthValidator validator;
        private readonly AuthService service;

        public AuthServiceTests()
        {
            this.context = new Mock<DatabaseContext>();
            this.validator = new AuthValidator();
            this.configuration = new Mock<IConfiguration>();
            this.service = new AuthService(this.context.Object, this.configuration.Object, this.validator);
        }

        [Fact]
        public void LoginTest()
        {
            var user = new User 
            {
                Id = Guid.NewGuid(),
                Username = "username",
                Password = "password",
                Roles = "Admin"
            };
            // var goodUserDto = new LoginUserDto  { Username = "username", Password = "password" };
            var badUsernameUserDto = new LoginUserDto  { Username = "badUsername", Password = "password" };
            var badPasswordUserDto = new LoginUserDto  { Username = "username", Password = "badPassword" };
            var badUserDto = new LoginUserDto { Username = "badusername", Password = "password" };

            this.context.Setup(x => x.Users).ReturnsDbSet(new List<User>{ user });
            this.configuration.Setup(x => x.GetSection("AppSettings:Key").Value).Returns("G2OmUU0zKe11cadv3zmehTpKXPS7V5wc8U6twl48qENoNxmAuVJ9wqDZ0usx9KxMngUndlNpRj8osPkN");

            // Test bad password case
            Assert.Throws<NotFoundException>(() => this.service.Login(badUserDto));

            // Test bad user case
            Assert.Throws<NotFoundException>(() => this.service.Login(badUserDto));

            // Test bad username case
            this.context.Setup(x => x.Users).ReturnsDbSet(new List<User>());
            Assert.Throws<NotFoundException>(() => this.service.Login(badUsernameUserDto));
        }
    }
}