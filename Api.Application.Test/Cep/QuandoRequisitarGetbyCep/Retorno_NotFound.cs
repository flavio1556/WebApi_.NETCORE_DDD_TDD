using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Cep.QuandoRequisitarGetbyCep
{
    public class Retorno_NotFound
    {
        private CepsController _controller;
        [Fact(DisplayName = "E possivel Realizar o Created")]
        public async Task E_Possivel_Inovcar_A_Controller_Create()
        {

            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Get(It.IsAny<string>())).Returns(Task.FromResult((CepDto)null)
           );

            _controller = new CepsController(serviceMock.Object);
            var result = await _controller.Get("13.481-001");
            Assert.True(result is NotFoundResult);
        }
    }
}
