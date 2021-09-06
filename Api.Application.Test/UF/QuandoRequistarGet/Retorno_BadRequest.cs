using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.UF.QuandoRequistarGet
{
    public class Retorno_BadRequest
    {
        private UfsController _controller;
        [Fact(DisplayName = "É possivel Realizar o get")]
        public async Task E_Possivel_Inovocar_a_Controller_get()
        {
            var serviceMock = new Mock<IUfService>();
            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UfDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo",
                    Sigla = "SP"
                }
                );
            _controller = new UfsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato invalido");
            var result = await _controller.Get(new Guid());
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }
    }
}
