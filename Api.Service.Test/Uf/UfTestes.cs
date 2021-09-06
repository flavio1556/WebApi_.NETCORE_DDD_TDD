using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Uf
{
    public class UfTestes
    {
        public static string Nome { get; set; }
        public static string Sigla { get; set; }
        public static Guid IdUF { get; set; }
        public List<UfDto> ListaUfDto = new List<UfDto>();
        public UfDto ufDto;

        public UfTestes()
        {
            IdUF = Guid.NewGuid();
            Sigla = Faker.Address.UsState().Substring(1, 3);
            Nome = Faker.Address.UsState();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UfDto()
                {
                    Id = Guid.NewGuid(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    Nome = Faker.Address.UsState(),
                };
                ListaUfDto.Add(dto);
            }
            ufDto = new UfDto()
            {
                Id = IdUF,
                Sigla = Sigla,
                Nome = Nome
            };
        }

    }
}
