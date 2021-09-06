using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Uf
{
    public class QuandoRequisitarUf : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Reaquisitar_Uf()
        {
            await AdicionarToken();
            //getall
            response = await client.GetAsync($"{hostApi}ufs");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listafromjson = JsonConvert.DeserializeObject<IEnumerable<UfDto>>(jsonResult);

            Assert.NotNull(listafromjson);
            Assert.True(listafromjson.Count() == 27);
            Assert.True(listafromjson.Where(r => r.Sigla == "SP").Count() == 1);

            var id = listafromjson.Where(r => r.Sigla == "SP").FirstOrDefault().Id;

            //get
            response = await client.GetAsync($"{hostApi}ufs/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<UfDto>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal("São Paulo", registroSelecionado.Nome);
            Assert.Equal("SP", registroSelecionado.Sigla);
        }
    }
}
