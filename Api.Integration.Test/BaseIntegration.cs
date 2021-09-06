using System.Net.Http;
using Api.Data.Context;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using application;
using Microsoft.AspNetCore.TestHost;
using System;
using Api.CrossCutting.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        public MyContext myContext { get; private set; }
        public HttpClient client { get; private set; }
        public IMapper mapper { get; private set; }
        public string hostApi { get; set; }
        public HttpResponseMessage response { get; set; }
        public BaseIntegration()
        {
            hostApi = "Http://localhost:5000/api/";
            var builder = new WebHostBuilder()
                       .UseEnvironment("Testing")
                       .UseStartup<Startup>();
            var server = new TestServer(builder);
            myContext = server.Host.Services.GetService(typeof(MyContext)) as MyContext;
            myContext.Database.Migrate();
            mapper = new AutoMapperFixture().GetMapper();
            client = server.CreateClient();
        }

        public void Dispose()
        {
            myContext.Dispose();
            client.Dispose();
        }
        public async Task AdicionarToken()
        {
            var loginDto = new LoginDto
            {
                Email = "Flavio1887@hotmail.com"
            };
            var resultLogin = await PostJsonAsync(loginDto, $"{hostApi}login", client);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResponseDto>(jsonLogin);
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginObject.acessToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                                                        loginObject.acessToken);
        }
        public static async Task<HttpResponseMessage> PostJsonAsync(object dataclass, string url, HttpClient client)
        {
            return await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(dataclass), System.Text.Encoding.UTF8, "application/json"));
        }
    }
    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile(new DtoToModelProfile());
                            cfg.AddProfile(new EntityToDtoProfile());
                            cfg.AddProfile(new ModelToEntityProfile());
                        });
            return config.CreateMapper();
        }
        public void Dispose()
        {

        }
    }

}
