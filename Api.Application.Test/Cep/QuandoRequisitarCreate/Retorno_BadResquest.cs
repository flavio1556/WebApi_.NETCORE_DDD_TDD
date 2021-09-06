using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Cep.QuandoRequisitarCreate
{
    public class Retorno_BadResquest
    {
        private CepsController _controller;
        [Fact(DisplayName = "E possivel Realizar o Created")]
        public async Task E_Possivel_Inovcar_A_Controller_Create()
        {

            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Post(It.IsAny<CepDtoCreate>())).ReturnsAsync(
                new CepDtoCreateResult
                {
                    Id = Guid.NewGuid(),
                    Cep = "13.481-001",
                    Logradouro = Faker.Address.StreetName(),
                    MunicipioId = Guid.NewGuid(),
                    Numero = Faker.RandomNumber.Next(0, 2000).ToString(),
                    CreateAt = DateTime.UtcNow
                }
            );
            _controller = new CepsController(serviceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost:5000");
            _controller.Url = url.Object;
            _controller.ModelState.AddModelError("Cep", "Cep Invalido");
            var CepDtoCreate = new CepDtoCreate
            {
                Cep = "13.481-001",
                Logradouro = Faker.Address.StreetName(),
                MunicipioID = Guid.NewGuid(),
                Numero = Faker.RandomNumber.Next(0, 2000).ToString(),
            };
            var result = await _controller.Post(CepDtoCreate);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
