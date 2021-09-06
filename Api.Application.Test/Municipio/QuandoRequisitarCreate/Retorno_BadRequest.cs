using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarCreate
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;
        [Fact(DisplayName = "E possivel Realizar o Created")]
        public async Task E_Possivel_Inovcar_A_Controller_Create()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Post(It.IsAny<MunicipioDtoCreate>())).ReturnsAsync(
                new MunicipioDtoCreateResult
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo",
                    CreateAt = DateTime.UtcNow
                }
            );
            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Nome", "É um Campo Obrigatorio");
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://Localhost:5000");

            var MunicipioDtoCreate = new MunicipioDtoCreate
            {
                Nome = "São Paulo",
                CodIbge = 1
            };
            var result = await _controller.Post(MunicipioDtoCreate);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
