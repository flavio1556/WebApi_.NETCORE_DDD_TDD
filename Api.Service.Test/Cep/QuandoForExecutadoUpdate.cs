using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoUpdate : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;
        [Fact(DisplayName = "É Possivel Executar o Metodo Update.")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Put(cepDtoUpdate)).ReturnsAsync(cepDtoUpdateResult);
            _service = _serviceMock.Object;

            var result = await _service.Put(cepDtoUpdate);
            Assert.NotNull(result);
            Assert.Equal(result.Logradouro, LogradouroCep);
            Assert.Equal(result.Cep, NomeCep);
            Assert.Equal(result.Numero, Numero);
            Assert.Equal(result.MunicipioId, MunicipioId);
        }
    }
}
