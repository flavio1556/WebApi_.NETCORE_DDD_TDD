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
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        public ServiceProvider _serviceProvider { get; set; }
        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }
        [Fact(DisplayName = "Crud Usuario")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_Crud_Usuario()
        {
            using (var contex = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repositorio = new UserImplementation(contex);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Nome = Faker.Name.FullName()
                };
                var _registroCriado = await _repositorio.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Email, _registroCriado.Email);
                Assert.Equal(_entity.Nome, _registroCriado.Nome);
                Assert.False(_registroCriado.Id == Guid.Empty);

                _entity.Nome = Faker.Name.First();
                var _registroAtualizado = await _repositorio.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.Email, _registroAtualizado.Email);
                Assert.Equal(_entity.Nome, _registroAtualizado.Nome);

                var _registroExiste = await _repositorio.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);


                var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Email, _registroSelecionado.Email);
                Assert.Equal(_registroAtualizado.Nome, _registroSelecionado.Nome);

                var _TodosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_TodosRegistros);
                Assert.True(_TodosRegistros.Count() > 1);

                var _removeu = await _repositorio.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_removeu);

                var _UsuarioPadrao = await _repositorio.FindByLogin("Flavio1887@hotmail.com");
                Assert.NotNull(_UsuarioPadrao);
                Assert.Equal("Flavio1887@hotmail.com".ToLower(), _UsuarioPadrao.Email.ToLower());
                Assert.Equal("Administrador", _UsuarioPadrao.Nome);

            }
        }

    }
}
