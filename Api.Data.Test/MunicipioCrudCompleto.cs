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
    public class MunicipioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvide;
        public MunicipioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }
        [Fact(DisplayName = "Crud de Municipio")]
        [Trait("Crud", "MunicipioEntity")]
        public async Task E_Possivel_Realizar_Crud()
        {
            using (var contex = _serviceProvide.GetService<MyContext>())
            {
                MunicipioImplementation _repositorio = new MunicipioImplementation(contex);
                MunicipioEntity _entity = new MunicipioEntity()
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
                };

                // Insert
                var _registroCriado = await _repositorio.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Nome, _registroCriado.Nome);
                Assert.Equal(_entity.CodIBGE, _registroCriado.CodIBGE);
                Assert.Equal(_entity.UfId, _registroCriado.UfId);
                Assert.False(_registroCriado.Id == Guid.Empty);

                //Update
                _entity.Nome = Faker.Address.City();
                _entity.Id = _registroCriado.Id;
                var _registroAtualizado = await _repositorio.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.Nome, _registroAtualizado.Nome);
                Assert.Equal(_entity.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_entity.UfId, _registroAtualizado.UfId);
                Assert.True(_registroAtualizado.Id == _entity.Id);

                //verifica a existencia
                var _registroExiste = await _repositorio.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                // seleciona by ID
                var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroSelecionado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroSelecionado.UfId, _registroAtualizado.UfId);
                Assert.Null(_registroAtualizado.Uf);

                //seleciona completo by ibge
                _registroSelecionado = await _repositorio.GetCompletoByIBGE(_registroAtualizado.CodIBGE);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroSelecionado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroSelecionado.UfId, _registroAtualizado.UfId);
                Assert.NotNull(_registroAtualizado.Uf);

                //seleciona completo by ibge
                _registroSelecionado = await _repositorio.GetCompleteByID(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroSelecionado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroSelecionado.UfId, _registroAtualizado.UfId);
                Assert.NotNull(_registroAtualizado.Uf);

                //select total
                var _totalRegistro = await _repositorio.SelectAsync();
                Assert.NotNull(_totalRegistro);
                Assert.True(_totalRegistro.Count() > 0);

                // delete
                var _removeu = await _repositorio.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_removeu);

                _totalRegistro = await _repositorio.SelectAsync();
                Assert.NotNull(_totalRegistro);
                Assert.True(_totalRegistro.Count() == 0);


            }
        }
    }
}
