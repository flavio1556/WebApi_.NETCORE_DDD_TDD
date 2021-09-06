using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class MunicipioMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel Mapear os Modelos de Municipio.")]
        public void E_Possivel_Mapear_Os_Modelos_Municipio()
        {
            var model = new MunicipioModel
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.UsState(),
                CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
            var listaEntity = new List<MunicipioEntity>();

            for (int i = 0; i < 5; i++)
            {
                var item = new MunicipioEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = Guid.NewGuid(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    Uf = new UfEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)
                    }

                };
                listaEntity.Add(item);
            }
            // model => entity
            var entity = Mapper.Map<MunicipioEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.CodIBGE, model.CodIBGE);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);


            //Entity => dto
            var municipioDto = Mapper.Map<MunicipioDto>(entity);
            Assert.Equal(municipioDto.Id, entity.Id);
            Assert.Equal(municipioDto.Nome, entity.Nome);
            Assert.Equal(municipioDto.CodIbge, entity.CodIBGE);
            Assert.Equal(municipioDto.UfId, entity.UfId);

            //maper Completo
            var MunicipioDtoCompleto = Mapper.Map<MunicipioDtoCompleto>(listaEntity.FirstOrDefault());

            Assert.Equal(MunicipioDtoCompleto.Id, listaEntity.FirstOrDefault().Id);
            Assert.Equal(MunicipioDtoCompleto.Nome, listaEntity.FirstOrDefault().Nome);
            Assert.Equal(MunicipioDtoCompleto.CodIbge, listaEntity.FirstOrDefault().CodIBGE);
            Assert.Equal(MunicipioDtoCompleto.UfId, listaEntity.FirstOrDefault().UfId);
            Assert.NotNull(MunicipioDtoCompleto.Uf);

            //lista
            var listadto = Mapper.Map<List<MunicipioDto>>(listaEntity);
            Assert.True(listadto.Count() == listaEntity.Count());
            for (int i = 0; i < listadto.Count(); i++)
            {
                Assert.Equal(listadto[i].Id, listaEntity[i].Id);
                Assert.Equal(listadto[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listadto[i].CodIbge, listaEntity[i].CodIBGE);
                Assert.Equal(listadto[i].UfId, listaEntity[i].UfId);

            }

            //maper Create Result
            var MunicipioDtoCreateResult = Mapper.Map<MunicipioDtoCreateResult>(entity);

            Assert.Equal(MunicipioDtoCreateResult.Id, entity.Id);
            Assert.Equal(MunicipioDtoCreateResult.Nome, entity.Nome);
            Assert.Equal(MunicipioDtoCreateResult.CodIbge, entity.CodIBGE);
            Assert.Equal(MunicipioDtoCreateResult.ufId, entity.UfId);
            Assert.Equal(MunicipioDtoCreateResult.CreateAt, entity.CreateAt);

            //maper update result
            var MunicipioDtoUpdateResult = Mapper.Map<MunicipioDtoUpdateResult>(entity);

            Assert.Equal(MunicipioDtoUpdateResult.Id, entity.Id);
            Assert.Equal(MunicipioDtoUpdateResult.Nome, entity.Nome);
            Assert.Equal(MunicipioDtoUpdateResult.CodIbge, entity.CodIBGE);
            Assert.Equal(MunicipioDtoUpdateResult.ufId, entity.UfId);
            Assert.Equal(MunicipioDtoUpdateResult.UpdateAt, entity.UpdateAt);

            //dto para model
            var municipioModel = Mapper.Map<MunicipioModel>(municipioDto);
            Assert.Equal(municipioModel.Id, municipioDto.Id);
            Assert.Equal(municipioModel.Nome, municipioDto.Nome);
            Assert.Equal(municipioModel.CodIBGE, municipioDto.CodIbge);
            Assert.Equal(municipioModel.UfId, municipioDto.UfId);


            var municipioDtoCreate = Mapper.Map<MunicipioDtoCreate>(municipioModel);

            Assert.Equal(municipioDtoCreate.Nome, municipioModel.Nome);
            Assert.Equal(municipioDtoCreate.CodIbge, municipioModel.CodIBGE);
            Assert.Equal(municipioDtoCreate.ufId, municipioModel.UfId);


            var MunicipioDtoUpdadte = Mapper.Map<MunicipioDtoUpdate>(municipioModel);

            Assert.Equal(MunicipioDtoUpdadte.Id, municipioModel.Id);
            Assert.Equal(MunicipioDtoUpdadte.Nome, municipioModel.Nome);
            Assert.Equal(MunicipioDtoUpdadte.CodIbge, municipioModel.CodIBGE);
            Assert.Equal(municipioDtoCreate.ufId, municipioModel.UfId);




        }
    }
}
