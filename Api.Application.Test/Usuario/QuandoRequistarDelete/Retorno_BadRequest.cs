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
    public class Retorno_BadRequest
    {
        private UsersController _Controller;

        [Fact(DisplayName = "É possivel Invoar o Controller DeletedBadResquest.")]
        public async Task E_Possivel_Invocar_a_Controller_DeletedBadResquest()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _Controller = new UsersController(serviceMock.Object);
            _Controller.ModelState.AddModelError("Id", "Formato invalido");

            var result = await _Controller.Delete(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_Controller.ModelState.IsValid);

        }
    }
}
