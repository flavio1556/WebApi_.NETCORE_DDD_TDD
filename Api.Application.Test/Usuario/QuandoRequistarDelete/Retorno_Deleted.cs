using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Usuario.QuandoRequistarDelete
{
    public class Retorno_Deleted
    {
        private UsersController _Controller;

        [Fact(DisplayName = "É possivel Invoar o Controller Deleted.")]
        public async Task E_Possivel_Invocar_a_Controller_Deleted()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _Controller = new UsersController(serviceMock.Object);


            var result = await _Controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);
            var resultvalue = ((OkObjectResult)result).Value;
            Assert.NotNull(resultvalue);
            Assert.True((Boolean)resultvalue)
          ;
        }
    }
}
