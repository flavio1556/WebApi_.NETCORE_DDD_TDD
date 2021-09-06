
using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGetCompleteByID : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;
        [Fact(DisplayName = "É Possivel Executar o Metodo Get complete by ID.")]
        public async Task E_Possivel_Executar_Metodo_Get_Complete_by_Id()
        {
            //Get
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompletoById(IdMunicipio)).ReturnsAsync(municipioDtoCompleto);
            _service = _serviceMock.Object;

            var result = await _service.GetCompletoById(IdMunicipio);
            Assert.NotNull(result);
            Assert.True(result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIbge);
            Assert.NotNull(result.Uf);
        }
    }
}
