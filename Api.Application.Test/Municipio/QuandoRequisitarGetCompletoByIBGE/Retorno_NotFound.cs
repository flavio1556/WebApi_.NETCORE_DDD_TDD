using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Municipio.QuandoRequisitarGetCompletoByIBGE
{
    public class Retorno_NotFound
    {

        private MunicipiosController _controller;
        [Fact(DisplayName = "E possivel Realizar o GetCompletoByIBGE")]
        public async Task E_Possivel_Inovcar_A_Controller_GetCompletoByIBGE()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.GetCompletoByIBGE(It.IsAny<int>())).Returns(Task.FromResult((MunicipioDtoCompleto)null)
            );

            _controller = new MunicipiosController(serviceMock.Object);
            var result = await _controller.GetCompletByIBGE(Faker.RandomNumber.Next(1, 1000));
            Assert.True(result is NotFoundResult);
        }
    }
}
