using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Cep.QuandoRequisitarGet
{
    public class Retorno_Ok
    {
        private CepsController _controller;
        [Fact(DisplayName = "E possivel Realizar o Created")]
        public async Task E_Possivel_Inovcar_A_Controller_Create()
        {

            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new CepDto
                {
                    Id = Guid.NewGuid(),
                    Cep = "13.481-001",
                    Logradouro = Faker.Address.StreetName(),
                    MunicipioId = Guid.NewGuid(),
                    Numero = Faker.RandomNumber.Next(0, 2000).ToString()
                }
            );
            _controller = new CepsController(serviceMock.Object);


            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);
        }
    }
}
