using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoGet : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;
        [Fact(DisplayName = "É Possivel Executar o Metodo Get.")]
        public async Task E_Possivel_Executar_Metodo_Get()
        {
            //Get
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(IdCep)).ReturnsAsync(CepDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(IdCep);
            Assert.NotNull(result);
            Assert.True(result.Id == IdCep);
            Assert.Equal(result.Logradouro, LogradouroCep);
            Assert.Equal(result.Cep, NomeCep);
            Assert.Equal(result.Numero, Numero);
            Assert.Equal(result.MunicipioId, MunicipioId);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((CepDto)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(IdCep);
            Assert.Null(_record);

            /// get by cep
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(NomeCep)).ReturnsAsync(CepDto);
            _service = _serviceMock.Object;

            var resultcep = await _service.Get(NomeCep);
            Assert.NotNull(result);
            Assert.True(result.Id == IdCep);
            Assert.Equal(result.Logradouro, LogradouroCep);
            Assert.Equal(result.Cep, NomeCep);
            Assert.Equal(result.Numero, Numero);
            Assert.Equal(result.MunicipioId, MunicipioId);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<string>())).Returns(Task.FromResult((CepDto)null));
            _service = _serviceMock.Object;

            var _recordcep = await _service.Get(IdCep);
            Assert.Null(_record);
        }
    }
}
