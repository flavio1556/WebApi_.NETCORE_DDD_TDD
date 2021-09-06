using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Uf;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UfMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel Mapear os Modelos de uf.")]
        public void E_Possivel_Mapear_Os_Modelos_uf()
        {
            var model = new UfModel
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.UsState(),
                Sigla = Faker.Address.UsState().Substring(1, 3),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
            var listaEntity = new List<UfEntity>();

            for (int i = 0; i < 5; i++)
            {
                var item = new UfEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow

                };
                listaEntity.Add(item);
            }

            //model => Entity
            var entity = Mapper.Map<UfEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.Sigla, model.Sigla);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            //Entity => dto
            var Ufdto = Mapper.Map<UfDto>(entity);
            Assert.Equal(Ufdto.Id, entity.Id);
            Assert.Equal(Ufdto.Nome, entity.Nome);
            Assert.Equal(Ufdto.Sigla, entity.Sigla);
            //lista
            var listadto = Mapper.Map<List<UfDto>>(listaEntity);
            Assert.True(listadto.Count() == listaEntity.Count());
            for (int i = 0; i < listadto.Count(); i++)
            {
                Assert.Equal(listadto[i].Id, listaEntity[i].Id);
                Assert.Equal(listadto[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listadto[i].Sigla, listaEntity[i].Sigla);
            }


            //dto para model
            var UfModel = Mapper.Map<UfModel>(Ufdto);
            Assert.Equal(UfModel.Id, Ufdto.Id);
            Assert.Equal(UfModel.Nome, Ufdto.Nome);
            Assert.Equal(UfModel.Sigla, UfModel.Sigla);

        }
    }
}
