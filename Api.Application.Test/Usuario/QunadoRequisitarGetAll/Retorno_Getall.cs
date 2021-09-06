using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QunadoRequisitarGetAll
{
    public class Retorno_Getall
    {

        private UsersController _Controller;

        [Fact(DisplayName = "É possivel Invoar o Controller Get.")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(new List<UserDto>
            {
               new UserDto
               {
                 Id = Guid.NewGuid(),
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow
               },
                new UserDto
               {
                 Id = Guid.NewGuid(),
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow
               },
                new UserDto
               {
                 Id = Guid.NewGuid(),
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow
               }
            });
            _Controller = new UsersController(serviceMock.Object);
            var result = await _Controller.GetAll();
            Assert.True(result is OkObjectResult);
            var resultvalue = ((OkObjectResult)result).Value as IEnumerable<UserDto>;
            Assert.NotNull(resultvalue);
            Assert.True(resultvalue.Count() == 3);



        }
    }
}
