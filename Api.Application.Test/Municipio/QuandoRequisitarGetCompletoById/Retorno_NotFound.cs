using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Municipio.QuandoRequisitarGetCompletoById
{
    public class Retorno_NotFound
    {

        private MunicipiosController _controller;
        [Fact(DisplayName = "E possivel Realizar o GetCompletoByID")]
        public async Task E_Possivel_Inovcar_A_Controller_GetCompletoByID()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.GetCompletoById(It.IsAny<Guid>())).Returns(Task.FromResult((MunicipioDtoCompleto)null)
            );

            _controller = new MunicipiosController(serviceMock.Object);
            var result = await _controller.GetCompleteById(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }
    }
}
