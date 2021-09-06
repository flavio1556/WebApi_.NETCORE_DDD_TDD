
using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGetCompleteByIBGE : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;
        [Fact(DisplayName = "É Possivel Executar o Metodo Get complete by IBGE.")]
        public async Task E_Possivel_Executar_Metodo_Get_Complete_by_IBGE()
        {
            //Get
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompletoByIBGE(CodigoIBGEMunicipio)).ReturnsAsync(municipioDtoCompleto);
            _service = _serviceMock.Object;

            var result = await _service.GetCompletoByIBGE(CodigoIBGEMunicipio);
            Assert.NotNull(result);
            Assert.True(result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIbge);
            Assert.NotNull(result.Uf);

        }
    }
}
