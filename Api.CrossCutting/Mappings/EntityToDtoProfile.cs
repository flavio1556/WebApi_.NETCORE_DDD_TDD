using AutoMapper;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Cep;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {

            #region User
            CreateMap<UserDto, UserEntity>()
            .ReverseMap();
            CreateMap<UserDtoCreateResult, UserEntity>()
            .ReverseMap();
            CreateMap<UserDtoUpdateResult, UserEntity>()
            .ReverseMap();
            #endregion

            #region  Uf
            CreateMap<UfDto, UfEntity>()
            .ReverseMap();
            #endregion 

            #region  Municipio
            CreateMap<MunicipioDto, MunicipioEntity>()
            .ReverseMap();
            CreateMap<MunicipioDtoCompleto, MunicipioEntity>()
            .ReverseMap();
            CreateMap<MunicipioDtoCreate, MunicipioEntity>()
            .ReverseMap();
            CreateMap<MunicipioDtoCreateResult, MunicipioEntity>()
           .ReverseMap();
            CreateMap<MunicipioDtoUpdate, MunicipioEntity>()
            .ReverseMap();
            CreateMap<MunicipioDtoUpdateResult, MunicipioEntity>()
            .ReverseMap();
            #endregion

            #region  Cep
            CreateMap<CepDto, CepEntity>()
           .ReverseMap();
            CreateMap<CepDtoCreate, CepEntity>()
            .ReverseMap();
            CreateMap<CepDtoCreateResult, CepEntity>()
            .ReverseMap();
            CreateMap<CepDtoUpdate, CepEntity>()
           .ReverseMap();
            CreateMap<CepDtoUpdateResult, CepEntity>()
            .ReverseMap();
            #endregion
        }
    }
}
