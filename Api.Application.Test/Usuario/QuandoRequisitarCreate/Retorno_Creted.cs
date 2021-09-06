using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarCrete
{
    public class Retorno_Creted
    {
        private UsersController _Controller;

        [Fact(DisplayName = "É possivel Invoar o Controller Create.")]
        public async Task E_Possivel_Invocar_a_Controller_Create()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            serviceMock.Setup(m => m.Post(It.IsAny<UserDtoCreate>())).ReturnsAsync(
                new UserDtoCreateResult
                {
                    Id = Guid.NewGuid(),
                    Nome = name,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                }
            );
            _Controller = new UsersController(serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost:5000");
            _Controller.Url = url.Object;
            var userdtocrete = new UserDtoCreate
            {
                Nome = name,
                Email = email
            };
            var result = await _Controller.Post(userdtocrete);
            Assert.True(result is CreatedResult);
            var resultvalue = ((CreatedResult)result).Value as UserDtoCreateResult;
            Assert.NotNull(resultvalue);
            Assert.Equal(userdtocrete.Nome, resultvalue.Nome);
            Assert.Equal(userdtocrete.Email, resultvalue.Email);
        }
    }
}
