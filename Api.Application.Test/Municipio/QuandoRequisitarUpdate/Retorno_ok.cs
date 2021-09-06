using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarUpdate
{
    public class Retorno_ok
    {
        private MunicipiosController _controller;
        [Fact(DisplayName = "E possivel Realizar o Update")]
        public async Task E_Possivel_Inovcar_A_Controller_Update()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Put(It.IsAny<MunicipioDtoUpdate>())).ReturnsAsync(
                new MunicipioDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo",
                    UpdateAt = DateTime.UtcNow
                }
            );
            _controller = new MunicipiosController(serviceMock.Object);


            var municipioDtoUpdate = new MunicipioDtoUpdate
            {
                Id = Guid.NewGuid(),
                Nome = "São Paulo",
                CodIbge = 1
            };
            var result = await _controller.Put(municipioDtoUpdate);
            Assert.True(result is OkObjectResult);
        }
    }
}
