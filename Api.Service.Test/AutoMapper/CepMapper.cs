using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Cep;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class CepMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel Mapear os Modelos de Municipio.")]
        public void E_Possivel_Mapear_Os_Modelos_Municipio()
        {
            var model = new CepModel
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
            var listaEntity = new List<CepEntity>();

            for (int c = 0; c < 5; c++)
            {
                var item = new CepEntity
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                    MunicipioId = Guid.NewGuid(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    Municipio = new MunicipioEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        CodIBGE = Faker.RandomNumber.Next(1, 10000),
                        UfId = Guid.NewGuid(),
                        Uf = new UfEntity
                        {
                            Id = Guid.NewGuid(),
                            Nome = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3)
                        }
                    }

                };
                listaEntity.Add(item);

                // model => entity
                var entity = Mapper.Map<CepEntity>(model);
                Assert.Equal(entity.Id, model.Id);
                Assert.Equal(entity.Cep, model.Cep);
                Assert.Equal(entity.Logradouro, model.Logradouro);
                Assert.Equal(entity.Numero, model.Numero);
                Assert.Equal(entity.CreateAt, model.CreateAt);
                Assert.Equal(entity.UpdateAt, model.UpdateAt);

                //Entity => dto
                var cepDto = Mapper.Map<CepDto>(entity);
                Assert.Equal(cepDto.Id, entity.Id);
                Assert.Equal(cepDto.Logradouro, entity.Logradouro);
                Assert.Equal(cepDto.Numero, entity.Numero);
                Assert.Equal(cepDto.Cep, entity.Cep);

                //lista
                var listadto = Mapper.Map<List<CepDto>>(listaEntity);
                Assert.True(listadto.Count() == listaEntity.Count());
                for (int i = 0; i < listadto.Count(); i++)
                {
                    Assert.Equal(listadto[i].Id, listaEntity[i].Id);
                    Assert.Equal(listadto[i].Cep, listaEntity[i].Cep);
                    Assert.Equal(listadto[i].Logradouro, listaEntity[i].Logradouro);
                    Assert.Equal(listadto[i].Numero, listaEntity[i].Numero);

                }

                //maper Completo
                var municipioDtoCompleto = Mapper.Map<CepDto>(listaEntity.FirstOrDefault());

                Assert.Equal(municipioDtoCompleto.Id, listaEntity.FirstOrDefault().Id);
                Assert.Equal(municipioDtoCompleto.Cep, listaEntity.FirstOrDefault().Cep);
                Assert.Equal(municipioDtoCompleto.Logradouro, listaEntity.FirstOrDefault().Logradouro);
                Assert.Equal(municipioDtoCompleto.Numero, listaEntity.FirstOrDefault().Numero);
                Assert.Equal(municipioDtoCompleto.MunicipioId, listaEntity.FirstOrDefault().MunicipioId);
                Assert.NotNull(municipioDtoCompleto.municipio);
                Assert.NotNull(municipioDtoCompleto.municipio.Uf);


                //maper Create Result
                var cepDtoCreateResult = Mapper.Map<CepDtoCreateResult>(entity);

                Assert.Equal(cepDtoCreateResult.Id, entity.Id);
                Assert.Equal(cepDtoCreateResult.Cep, entity.Cep);
                Assert.Equal(cepDtoCreateResult.Logradouro, entity.Logradouro);
                Assert.Equal(cepDtoCreateResult.MunicipioId, entity.MunicipioId);
                Assert.Equal(cepDtoCreateResult.CreateAt, entity.CreateAt);

                //maper update result
                var cepDtoUpdateResult = Mapper.Map<CepDtoUpdateResult>(entity);

                Assert.Equal(cepDtoUpdateResult.Id, entity.Id);
                Assert.Equal(cepDtoUpdateResult.Cep, entity.Cep);
                Assert.Equal(cepDtoUpdateResult.Logradouro, entity.Logradouro);
                Assert.Equal(cepDtoUpdateResult.Numero, entity.Numero);
                Assert.Equal(cepDtoUpdateResult.MunicipioId, entity.MunicipioId);
                Assert.Equal(cepDtoUpdateResult.UpdateAt, entity.UpdateAt);

                //dto para model
                var cepModel = Mapper.Map<CepModel>(cepDto);
                Assert.Equal(cepModel.Id, cepDto.Id);
                Assert.Equal(cepModel.Cep, cepDto.Cep);
                Assert.Equal(cepModel.Logradouro, cepDto.Logradouro);
                Assert.Equal(cepModel.Numero, cepDto.Numero);
                Assert.Equal(cepModel.MunicipioID, cepDto.MunicipioId);


                var cepDtoCreate = Mapper.Map<CepDtoCreate>(cepModel);

                Assert.Equal(cepDtoCreate.Cep, cepModel.Cep);
                Assert.Equal(cepDtoCreate.Logradouro, cepModel.Logradouro);
                Assert.Equal(cepDtoCreate.Numero, cepModel.Numero);


                var cepDtoUpdadte = Mapper.Map<CepDtoUpdate>(cepModel);

                Assert.Equal(cepDtoUpdadte.Cep, cepModel.Cep);
                Assert.Equal(cepDtoUpdadte.Logradouro, cepModel.Logradouro);
                Assert.Equal(cepDtoUpdadte.Numero, cepModel.Numero);
                Assert.Equal(cepDtoUpdadte.Id, cepModel.Id);
                Assert.Equal(cepDtoUpdadte.MunicipioId, cepModel.MunicipioID);

            }
        }
    }
}
