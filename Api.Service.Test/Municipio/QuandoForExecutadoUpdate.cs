using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoUpdate : MunicipioTestes
    {

        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;
        [Fact(DisplayName = "É Possivel Executar o Metodo Update.")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {


            //update
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Put(municipioDtoUpdate)).ReturnsAsync(municipioDtoUpdateResult);
            _service = _serviceMock.Object;

            var resultUpdate = await _service.Put(municipioDtoUpdate);
            Assert.NotNull(resultUpdate);
            Assert.Equal(municipioDtoUpdate.Nome, resultUpdate.Nome);
            Assert.Equal(municipioDtoUpdate.CodIbge, resultUpdate.CodIbge);

        }

    }
}
