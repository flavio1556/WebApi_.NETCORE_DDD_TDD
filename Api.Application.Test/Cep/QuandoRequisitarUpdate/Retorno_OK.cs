using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequisitarUpdate
{
    public class Retorno_OK
    {
        private CepsController _controller;
        [Fact(DisplayName = "E possivel Realizar o Update")]
        public async Task E_Possivel_Inovcar_A_Controller_Update()
        {

            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Put(It.IsAny<CepDtoUpdate>())).ReturnsAsync(
                new CepDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Cep = "13.481-001",
                    Logradouro = Faker.Address.StreetName(),
                    MunicipioId = Guid.NewGuid(),
                    Numero = Faker.RandomNumber.Next(0, 2000).ToString(),
                    UpdateAt = DateTime.UtcNow
                }
            );
            _controller = new CepsController(serviceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost:5000");
            _controller.Url = url.Object;

            var CepDtoUpdate = new CepDtoUpdate
            {
                Cep = "13.481-001",
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(0, 2000).ToString(),
            };
            var result = await _controller.Put(CepDtoUpdate);
            Assert.True(result is OkObjectResult);
        }
    }
}
