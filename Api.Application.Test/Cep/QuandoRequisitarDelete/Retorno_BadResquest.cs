using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Cep.QuandoRequisitarDelete
{
    public class Retorno_BadResquest
    {
        private CepsController _controller;
        [Fact(DisplayName = "E possivel Realizar o Created")]
        public async Task E_Possivel_Inovcar_A_Controller_Create()
        {

            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _controller = new CepsController(serviceMock.Object);

            _controller.ModelState.AddModelError("Id", "id Invalido");
            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
