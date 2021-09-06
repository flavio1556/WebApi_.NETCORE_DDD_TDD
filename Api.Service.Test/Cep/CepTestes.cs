using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Cep;

namespace Api.Service.Test.Cep
{
    public class CepTestes
    {

        public static string NomeCep { get; set; }
        public static string LogradouroCep { get; set; }
        public static string Numero { get; set; }
        public static Guid IdCep { get; set; }
        public static Guid MunicipioId { get; set; }
        public List<CepDto> ListaDTO = new List<CepDto>();
        public CepDto CepDto;
        public CepDtoCreate cepDtoCreate;
        public CepDtoCreateResult CreateDtoResult;
        public CepDtoUpdate cepDtoUpdate;
        public CepDtoUpdateResult cepDtoUpdateResult;
        public CepTestes()
        {
            IdCep = Guid.NewGuid();
            NomeCep = "13.481-001";
            Numero = Faker.RandomNumber.Next(0, 2000).ToString();
            LogradouroCep = Faker.Address.StreetName();
            MunicipioId = Guid.NewGuid();
            for (int i = 0; i < 10; i++)
            {
                var dto = new CepDto()
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.Address.StreetSuffix(),
                    Numero = Faker.RandomNumber.Next(0, 2000).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    MunicipioId = Guid.NewGuid()
                };
                ListaDTO.Add(dto);
            }
            CepDto = new CepDto
            {
                Id = IdCep,
                Cep = NomeCep,
                Numero = Numero,
                Logradouro = LogradouroCep,
                MunicipioId = MunicipioId
            };
            cepDtoCreate = new CepDtoCreate
            {
                Cep = NomeCep,
                Numero = Numero,
                Logradouro = LogradouroCep,
                MunicipioID = MunicipioId
            };
            CreateDtoResult = new CepDtoCreateResult
            {
                Id = IdCep,
                Cep = NomeCep,
                Numero = Numero,
                Logradouro = LogradouroCep,
                MunicipioId = MunicipioId,
                CreateAt = DateTime.UtcNow
            };
            cepDtoUpdate = new CepDtoUpdate()
            {
                Id = IdCep,
                Cep = NomeCep,
                Numero = Numero,
                Logradouro = LogradouroCep,
                MunicipioId = MunicipioId
            };
            cepDtoUpdateResult = new CepDtoUpdateResult
            {
                Id = IdCep,
                Cep = NomeCep,
                Numero = Numero,
                Logradouro = LogradouroCep,
                MunicipioId = MunicipioId,
                UpdateAt = DateTime.UtcNow
            };
        }

    }
}
