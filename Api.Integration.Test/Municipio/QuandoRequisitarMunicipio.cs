using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;
using Newtonsoft.Json;
using Xunit;
namespace Api.Integration.Test.Municipio
{
    public class QuandoRequisitarMunicipio : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Reaquisitar_Municipios()
        {
            await AdicionarToken();

            var MunicipioDtoCreate = new MunicipioDtoCreate()
            {
                Nome = "São Paulo",
                CodIbge = 3550308,
                ufId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
            };

            //post
            var response = await PostJsonAsync(MunicipioDtoCreate, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("São Paulo", registroPost.Nome);
            Assert.Equal(3550308, registroPost.CodIbge);
            Assert.True(registroPost.Id != default(Guid));
            //getall
            response = await client.GetAsync($"{hostApi}municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listafromjson = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResult);
            Assert.NotNull(listafromjson);
            Assert.True(listafromjson.Count() > 0);
            Assert.True(listafromjson.Where(r => r.Id == registroPost.Id).Count() == 1);
            //put
            var MunicipioDtoUpdate = new MunicipioDtoUpdate
            {
                Id = registroPost.Id,
                Nome = "Limeira",
                CodIbge = 3526902,
                ufId = registroPost.ufId
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(MunicipioDtoUpdate), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}municipios", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(jsonResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(registroPost.Nome, registroAtualizado.Nome);
            Assert.NotEqual(registroPost.CodIbge, registroAtualizado.CodIbge);

            //GET Id
            response = await client.GetAsync($"{hostApi}municipios/{registroPost.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<MunicipioDto>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroSelecionado.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionado.CodIbge, registroAtualizado.CodIbge);

            //GET Complete/Id
            response = await client.GetAsync($"{hostApi}municipios/Complete/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoCompleto = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(registroSelecionadoCompleto);
            Assert.Equal(registroSelecionadoCompleto.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionadoCompleto.CodIbge, registroAtualizado.CodIbge);
            Assert.NotNull(registroSelecionadoCompleto.Uf);
            Assert.Equal("São Paulo", registroSelecionadoCompleto.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoCompleto.Uf.Sigla);

            //GET byIBGE/CodIBGE
            response = await client.GetAsync($"{hostApi}municipios/byIBGE/{registroAtualizado.CodIbge}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoIBGECompleto = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(registroSelecionadoIBGECompleto);
            Assert.Equal(registroSelecionadoIBGECompleto.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionadoIBGECompleto.CodIbge, registroAtualizado.CodIbge);
            Assert.NotNull(registroSelecionadoIBGECompleto.Uf);
            Assert.Equal("São Paulo", registroSelecionadoIBGECompleto.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoIBGECompleto.Uf.Sigla);

            //DELETE
            response = await client.DeleteAsync($"{hostApi}municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //GET ID depois do DELETE
            response = await client.GetAsync($"{hostApi}municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}
