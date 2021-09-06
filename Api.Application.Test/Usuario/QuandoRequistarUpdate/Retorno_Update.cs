using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Usuario.QuandoRequistarUpdate
{
    public class Retorno_Update
    {
        private UsersController _Controller;

        [Fact(DisplayName = "É possivel Invoar o Controller Update.")]
        public async Task E_Possivel_Invocar_a_Controller_Update()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            serviceMock.Setup(m => m.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(
                new UserDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Nome = name,
                    Email = email,
                    UpdateAt = DateTime.UtcNow
                }
            );
            _Controller = new UsersController(serviceMock.Object);
            var userdtoUpdate = new UserDtoUpdate
            {
                Id = Guid.NewGuid(),
                Nome = name,
                Email = email
            };
            var result = await _Controller.Put(userdtoUpdate);
            Assert.True(result is OkObjectResult);
            var resultvalue = ((OkObjectResult)result).Value as UserDtoUpdateResult;
            Assert.NotNull(resultvalue);
            Assert.Equal(userdtoUpdate.Nome, resultvalue.Nome);
            Assert.Equal(userdtoUpdate.Email, resultvalue.Email);
        }
    }
}
