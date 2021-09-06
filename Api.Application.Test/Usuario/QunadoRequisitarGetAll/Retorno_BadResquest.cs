using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Usuario.QunadoRequisitarGetAll
{
    public class Retorno_BadResquest
    {
        private UsersController _Controller;

        [Fact(DisplayName = "É possivel Invoar o Controller GetAllBadResquest.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAllBadResquest()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
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
            _Controller.ModelState.AddModelError("Id", "Formato invalido");

            var result = await _Controller.Delete(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_Controller.ModelState.IsValid);

        }
    }
}
