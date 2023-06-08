using api.Repositories;
using api.Services;
using api.Validators;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;
using Microsoft.Extensions.Configuration;
using api.Models;
using api.Exceptions;

namespace tests.PrivateDestinationService
{
    public class PrivateDestinationServiceTests

    {

        private readonly Mock<DatabaseContext> context;
        private readonly Mock<IPermission> permission;
        private readonly PrivateDestinationValidator validator;
        private readonly PrivateDestinationsService service;

        public PrivateDestinationServiceTests()
        {
            this.context = new Mock<DatabaseContext>();
            this.validator = new PrivateDestinationValidator();
            this.permission = new Mock<IPermission>();
            this.service = new PrivateDestinationsService(this.context.Object, this.permission.Object, this.validator);
        }

        [Fact]
        public void LoginTest()
        {
            var privateDestination = new PrivateDestination 
            {
                Id = Guid.NewGuid(),
                Geolocation = "asdfasdf",
                Title = "Title",
                Image = "image",
                Description = "description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            
            // this.context.Setup(x => x.Users).ReturnsDbSet(new List<PrivateDestination>{  });
            this.permission.Setup(x => x.Check(It.IsAny<Guid>()));

        }
    }
}