using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Municipio.QuandoRequisitarGetall
{
    public class Retorno_ok
    {
        private MunicipiosController _controller;
        [Fact(DisplayName = "E possivel Realizar o GetAll")]
        public async Task E_Possivel_Inovcar_A_Controller_GetAll()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<MunicipioDto>
                {
                new MunicipioDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo",
                },
                new MunicipioDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "Salvador",
                }
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);
        }
    }
}
