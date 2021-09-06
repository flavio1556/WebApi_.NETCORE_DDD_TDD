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
    public class CepCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvide;
        public CepCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }
        [Fact(DisplayName = "Crud de Cep")]
        [Trait("Crud", "CepEntity")]
        public async Task E_Possivel_Realizar_Crud()
        {

            using (var contex = _serviceProvide.GetService<MyContext>())
            {

                MunicipioImplementation _repositorioMunicipio = new MunicipioImplementation(contex);

                MunicipioEntity _entityMunicipio = new MunicipioEntity()
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
                };
                var _registroCriadoMunicipio = await _repositorioMunicipio.InsertAsync(_entityMunicipio);
                Assert.NotNull(_registroCriadoMunicipio);
                Assert.Equal(_entityMunicipio.Nome, _registroCriadoMunicipio.Nome);
                Assert.Equal(_entityMunicipio.CodIBGE, _registroCriadoMunicipio.CodIBGE);
                Assert.Equal(_entityMunicipio.UfId, _registroCriadoMunicipio.UfId);
                Assert.False(_registroCriadoMunicipio.Id == Guid.Empty);
                CepImplementation _repositorio = new CepImplementation(contex);
                CepEntity _entity = new CepEntity
                {
                    Cep = "13.481-001",
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(0, 2000).ToString(),
                    MunicipioId = _registroCriadoMunicipio.Id
                };

                //Insert
                var _registroCriado = await _repositorio.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Cep, _registroCriado.Cep);
                Assert.Equal(_entity.Logradouro, _registroCriado.Logradouro);
                Assert.Equal(_entity.Numero, _registroCriado.Numero);
                Assert.Equal(_entity.MunicipioId, _registroCriado.MunicipioId);
                Assert.False(_registroCriado.Id == Guid.Empty);

                //update
                _entity.Logradouro = Faker.Address.StreetName();
                var _registroAtualizado = await _repositorio.UpdateAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Cep, _registroCriado.Cep);
                Assert.Equal(_entity.Logradouro, _registroCriado.Logradouro);
                Assert.Equal(_entity.Numero, _registroCriado.Numero);
                Assert.Equal(_entity.MunicipioId, _registroCriado.MunicipioId);
                Assert.True(_registroCriado.Id == _entity.Id);

                //existe
                var _registroExiste = await _repositorio.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                // seleção by id                              SelectAsync
                var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Cep, _registroSelecionado.Cep);
                Assert.Equal(_registroAtualizado.Logradouro, _registroSelecionado.Logradouro);
                Assert.Equal(_registroAtualizado.Numero, _registroSelecionado.Numero);
                Assert.Equal(_registroAtualizado.MunicipioId, _registroSelecionado.MunicipioId);

                //seleção by cep
                _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Cep);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Cep, _registroSelecionado.Cep);
                Assert.Equal(_registroAtualizado.Logradouro, _registroSelecionado.Logradouro);
                Assert.Equal(_registroAtualizado.Numero, _registroSelecionado.Numero);
                Assert.Equal(_registroAtualizado.MunicipioId, _registroSelecionado.MunicipioId);
                Assert.NotNull(_registroSelecionado.Municipio);
                Assert.NotNull(_registroSelecionado.Municipio.Uf);

                // seleçao total
                var _todosRegistro = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistro);
                Assert.True(_todosRegistro.Count() > 0);
                //delete
                var _removeu = await _repositorio.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_removeu);

                _todosRegistro = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistro);
                Assert.True(_todosRegistro.Count() == 0);

            }
        }
    }
}
