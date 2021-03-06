using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
namespace Api.Data.Test
{
    public class UfGets : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvide;
        public UfGets(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }
        [Fact(DisplayName = "Get de UF")]
        [Trait("Gets", "UfEntity")]
        public async Task E_Possivel_Realizar_Gets_UF()
        {
            using (var contex = _serviceProvide.GetService<MyContext>())
            {
                UfImplementation _repositorio = new UfImplementation(contex);
                UfEntity _entity = new UfEntity()
                {
                    Id = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                    Sigla = "SP",
                    Nome = "São Paulo",
                    CreateAt = DateTime.UtcNow
                };
                var _registroExiste = await _repositorio.ExistAsync(_entity.Id);
                Assert.True(_registroExiste);
                var _registroSelecionado = await _repositorio.SelectAsync(_entity.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_entity.Sigla, _registroSelecionado.Sigla);
                Assert.Equal(_entity.Nome, _registroSelecionado.Nome);
                Assert.Equal(_entity.Id, _registroSelecionado.Id);

                var _todosRegistro = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistro);
                Assert.True(_todosRegistro.Count() == 27);
            }

        }
    }
}
