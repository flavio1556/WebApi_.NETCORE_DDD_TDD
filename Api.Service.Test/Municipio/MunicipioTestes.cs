using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Municipio
{
    public class MunicipioTestes
    {
        public static string NomeMunicipio { get; set; }
        public static int CodigoIBGEMunicipio { get; set; }
        public static string NomeMunicipioAlterado { get; set; }
        public static int CodigoIBGEMunicipioAlterado { get; set; }
        public static Guid IdMunicipio { get; set; }
        public static Guid IdUf { get; set; }

        public List<MunicipioDto> ListaDto = new List<MunicipioDto>();
        public MunicipioDto MunicipioDto;

        public MunicipioDtoCompleto municipioDtoCompleto;
        public MunicipioDtoCreate municipioDtoCreate;
        public MunicipioDtoCreateResult municipioDtoCreateResult;
        public MunicipioDtoUpdate municipioDtoUpdate;
        public MunicipioDtoUpdateResult municipioDtoUpdateResult;
        public MunicipioTestes()
        {
            IdMunicipio = Guid.NewGuid();
            NomeMunicipio = Faker.Address.City();
            CodigoIBGEMunicipio = Faker.RandomNumber.Next(1, 10000);
            NomeMunicipioAlterado = Faker.Address.City();
            CodigoIBGEMunicipioAlterado = Faker.RandomNumber.Next(1, 10000);
            IdUf = Guid.NewGuid();

            for (int i = 0; i < 10; i++)
            {
                var dto = new MunicipioDto()
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.City(),
                    CodIbge = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid()
                };
                ListaDto.Add(dto);
            }

            MunicipioDto = new MunicipioDto()
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIbge = CodigoIBGEMunicipio,
                UfId = IdUf
            };
            municipioDtoCompleto = new MunicipioDtoCompleto()
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIbge = CodigoIBGEMunicipio,
                UfId = IdUf,
                Uf = new UfDto()
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3)
                }
            };
            municipioDtoCreate = new MunicipioDtoCreate()
            {
                Nome = NomeMunicipio,
                CodIbge = CodigoIBGEMunicipio,
                ufId = IdUf
            };
            municipioDtoCreateResult = new MunicipioDtoCreateResult
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIbge = CodigoIBGEMunicipio,
                ufId = IdUf,
                CreateAt = DateTime.UtcNow
            };
            municipioDtoUpdate = new MunicipioDtoUpdate
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIbge = CodigoIBGEMunicipio,

            };

            municipioDtoUpdateResult = new MunicipioDtoUpdateResult
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIbge = CodigoIBGEMunicipio,
                ufId = IdUf,
                UpdateAt = DateTime.UtcNow
            };
        }
    }

}

