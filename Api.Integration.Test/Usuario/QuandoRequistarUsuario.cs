using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Newtonsoft.Json;
using Xunit;
using System.Net.Http;
using System.Text;

namespace Api.Integration.Test.Usuario
{
    public class QuandoRequistarUsuario : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Usuario()
        {
            await AdicionarToken();
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();
            var userdto = new UserDtoCreate
            {
                Nome = _name,
                Email = _email
            };
            //post
            var response = await PostJsonAsync(userdto, $"{hostApi}Users", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<UserDtoCreateResult>(postResult);
            Assert.NotNull(registroPost);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, registroPost.Nome);
            Assert.Equal(_email, registroPost.Email);
            Assert.True(registroPost.Id != default(Guid));

            //get all
            response = await client.GetAsync($"{hostApi}Users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listafromjson = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult);
            Assert.NotNull(listafromjson);
            Assert.True(listafromjson.Count() > 0);
            Assert.True(listafromjson.Where(x => x.Id == registroPost.Id).Any());
            var updateUserDto = new UserDtoUpdate()
            {
                Id = registroPost.Id,
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };
            //put
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateUserDto),
            Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}Users", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<UserDtoUpdateResult>(jsonResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(registroPost.Nome, registroAtualizado.Nome);
            Assert.NotEqual(registroPost.Email, registroAtualizado.Email);
            //get id
            response = await client.GetAsync($"{hostApi}Users/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<UserDtoUpdateResult>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroSelecionado.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionado.Email, registroAtualizado.Email);
            //delete
            response = await client.DeleteAsync($"{hostApi}Users/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //get id depos do delete 
            response = await client.GetAsync($"{hostApi}Users/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
