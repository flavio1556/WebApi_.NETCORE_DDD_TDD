using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecultadoDelete : UsuarioTeste
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;
        [Fact(DisplayName = "É possivel executar o Delete.")]
        public async Task E_Possivel_Execultar_Metodo_Delete()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;
            var result = await _service.Delete(IdUsuario);

            Assert.True(result);


            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;
            var resultfalse = await _service.Delete(IdUsuario);

            Assert.False(resultfalse);

        }
    }
}
