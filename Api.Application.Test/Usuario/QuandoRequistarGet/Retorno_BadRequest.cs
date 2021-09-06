using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Usuario.QuandoRequistarGet
{
    public class Retorno_BadRequest
    {
        private UsersController _Controller;

        [Fact(DisplayName = "É possivel Invoar o Controller GetBadResquest.")]
        public async Task E_Possivel_Invocar_a_Controller_GetBadResquest()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(new UserDto
            {
                Id = Guid.NewGuid(),
                Nome = name,
                Email = email,
                CreateAt = DateTime.UtcNow
            });
            _Controller = new UsersController(serviceMock.Object);
            _Controller.ModelState.AddModelError("Id", "Formato invalido");

            var result = await _Controller.Get(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_Controller.ModelState.IsValid);

        }
    }
}
