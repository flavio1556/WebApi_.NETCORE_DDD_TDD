using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel Mapear os Modelos.")]
        public void E_Possivel_Mapear_Os_Modelos()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow

            };
            var listaEntity = new List<UserEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow

                };
                listaEntity.Add(item);

            }
            // model => entity
            var Entity = Mapper.Map<UserEntity>(model);
            Assert.Equal(Entity.Id, model.Id);
            Assert.Equal(Entity.Nome, model.Nome);
            Assert.Equal(Entity.Email, model.Email);
            Assert.Equal(Entity.CreateAt, model.CreateAt);
            Assert.Equal(Entity.UpdateAt, model.UpdateAt);


            // entity => dto
            var userDto = Mapper.Map<UserDto>(Entity);
            Assert.Equal(userDto.Id, Entity.Id);
            Assert.Equal(userDto.Nome, Entity.Nome);
            Assert.Equal(userDto.Email, Entity.Email);
            Assert.Equal(userDto.CreateAt, Entity.CreateAt);

            //lista
            var listadto = Mapper.Map<List<UserDto>>(listaEntity);
            Assert.True(listadto.Count() == listaEntity.Count());
            for (int i = 0; i < listadto.Count(); i++)
            {
                Assert.Equal(listadto[i].Id, listaEntity[i].Id);
                Assert.Equal(listadto[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listadto[i].Email, listaEntity[i].Email);
                Assert.Equal(listadto[i].CreateAt, listaEntity[i].CreateAt);

            }
            //maper create result
            var userDtoCreateResult = Mapper.Map<UserDtoCreateResult>(Entity);

            Assert.Equal(userDtoCreateResult.Id, Entity.Id);
            Assert.Equal(userDtoCreateResult.Nome, Entity.Nome);
            Assert.Equal(userDtoCreateResult.Email, Entity.Email);
            Assert.Equal(userDtoCreateResult.CreateAt, Entity.CreateAt);
            //maper update result
            var userDtoUpdateResult = Mapper.Map<UserDtoUpdateResult>(Entity);

            Assert.Equal(userDtoUpdateResult.Id, Entity.Id);
            Assert.Equal(userDtoUpdateResult.Nome, Entity.Nome);
            Assert.Equal(userDtoUpdateResult.Email, Entity.Email);
            Assert.Equal(userDtoUpdateResult.UpdateAt, Entity.UpdateAt);


            //dto para model
            var UserModel = Mapper.Map<UserModel>(userDto);
            Assert.Equal(UserModel.Id, userDto.Id);
            Assert.Equal(UserModel.Nome, userDto.Nome);
            Assert.Equal(UserModel.Email, userDto.Email);
            Assert.Equal(UserModel.CreateAt, userDto.CreateAt);

            var UserDtoCrete = Mapper.Map<UserDtoCreate>(UserModel);

            Assert.Equal(UserDtoCrete.Nome, UserModel.Nome);
            Assert.Equal(UserDtoCrete.Email, UserModel.Email);

            var UserDtoUpdadte = Mapper.Map<UserDtoUpdate>(UserModel);

            Assert.Equal(UserDtoUpdadte.Id, userDto.Id);
            Assert.Equal(UserDtoUpdadte.Nome, userDto.Nome);
            Assert.Equal(UserDtoUpdadte.Email, userDto.Email);


        }
    }
}

